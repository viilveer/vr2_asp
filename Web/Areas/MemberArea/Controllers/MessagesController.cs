﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BLL.Helpers.Factories;
using BLL.ViewModels.Message;
using Domain;
using Domain.Identity;
using Interfaces.Repositories;
using Interfaces.UOW;
using Microsoft.AspNet.Identity;
using PagedList;
using BLL.ViewModels.MessageThread;
using Web.Controllers;
using CreateModel = BLL.ViewModels.Message.CreateModel;
using DetailsModel = BLL.ViewModels.MessageThread.DetailsModel;

namespace Web.Areas.MemberArea.Controllers
{
    [Authorize(Roles = "User")]
    public class MessagesController : BaseController
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly BaseIUOW _uow;

        public MessagesController(BaseIUOW uow)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
        }

        // GET: MemberArea/Messages
        public ActionResult Index(IndexModel vm)
        {
            int totalItemCount;
            string realSortProperty;

            // if not set, set base values
            vm.PageNumber = vm.PageNumber ?? 1;
            vm.PageSize = vm.PageSize ?? 25;

            var res = _uow.GetRepository<IMessageThreadRepository>().GetUserThreads(User.Identity.GetUserId<int>(), vm.SortProperty, vm.PageNumber.Value - 1, vm.PageSize.Value, out totalItemCount, out realSortProperty);

            vm.SortProperty = realSortProperty;

            // https://github.com/kpi-ua/X.PagedList
            vm.Messages = new StaticPagedList<MessageThread>(res, vm.PageNumber.Value, vm.PageSize.Value, totalItemCount);

            return View(vm);
        }

        // TODO :: finish
        // GET: MemberArea/Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageThread messageThread = _uow.GetRepository<IMessageThreadRepository>()
                .GetUserThread((int)id, User.Identity.GetUserId<int>());

            if (messageThread == null)
            {
                return HttpNotFound();
            }


            DetailsModel detailsModel = new DetailsModel()
            {
                Author = messageThread.Author.Email, // TODO :: fix
                Title = messageThread.Title,
                NewMessageModel = new CreateModel(),
                DetailsModels = _uow.GetRepository<IMessageRepository>()
                    .GetAllByThreadIdAndUserId(messageThread.MessageThreadId, User.Identity.GetUserId<int>())
                    .Select(DetailsModelFactory.CreateFromMessage)
                    .ToList(),
            };

            return View(detailsModel);
        }

        // TODO :: finish
        // GET: MemberArea/Messages/Details/5
        [HttpPost]
        public ActionResult Details(int? id, DetailsModel detailsModel)
        {
            int activeUserId = Convert.ToInt32(User.Identity.GetUserId());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageThread messageThread = _uow.GetRepository<IMessageThreadRepository>()
                .GetUserThread((int)id, activeUserId);

            if (messageThread == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                Message message = detailsModel.NewMessageModel.GetMessage();
                message.MessageThreadId = messageThread.MessageThreadId; // TODO :: FIX
                message.AuthorId = activeUserId;
                _uow.GetRepository<IMessageRepository>().Add(message);
                _uow.Commit();
                return RedirectToAction("Details", new {id = messageThread.MessageThreadId});
            }


            detailsModel.Author = messageThread.Author.Email; // TODO :: fix
            detailsModel.Title = messageThread.Title;
            detailsModel.DetailsModels = _uow.GetRepository<IMessageRepository>()
                .GetAllByThreadIdAndUserId(messageThread.MessageThreadId, User.Identity.GetUserId<int>())
                .Select(DetailsModelFactory.CreateFromMessage)
                .ToList();

            return View(detailsModel);
        }

        // GET: MemberArea/Messages/Create
        public ActionResult Create(int? receiverId)
        {
            UserInt user = _uow.GetRepository<IUserIntRepository>().GetById(receiverId);
            if (user == null)
            {
                return HttpNotFound();
            }

            BLL.ViewModels.MessageThread.CreateModel createModel = new BLL.ViewModels.MessageThread.CreateModel();
            return View(createModel);
        }

        // POST: MemberArea/Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Text")] BLL.ViewModels.MessageThread.CreateModel createModel, int? receiverId)
        {
            UserInt user = _uow.GetRepository<IUserIntRepository>().GetById(receiverId);
            if (user == null && User.Identity.GetUserId<int>() != receiverId)
            {
                return HttpNotFound();
            }


            if (ModelState.IsValid)
            {

                UserInt sender = UserIntFactory.CreateFromIdentity(_uow, User);
                NewThreadModel newThreadModel = new NewThreadModel(sender, user);
                newThreadModel.Prepare(createModel.Title, createModel.Text);

                int id = _uow.GetRepository<IMessageThreadRepository>().Add(newThreadModel.MessageThread);
                _uow.Commit();

                Message message = newThreadModel.Message;
                message.MessageThreadId = id;
                message.AuthorId = newThreadModel.MessageThread.AuthorId;
                message.Status = MessageStatus.New;

                int messageId = _uow.GetRepository<IMessageRepository>().Add(message);

                foreach (var messageReceiver in newThreadModel.MessageReceivers)
                {
                    messageReceiver.MessageId = messageId;
                    _uow.GetRepository<IMessageReceiverRepository>().Add(messageReceiver);
                }

                foreach (var messageThreadReceiver in newThreadModel.MessageThreadReceivers)
                {
                    messageThreadReceiver.MessageThreadId = id;
                    _uow.GetRepository<IMessageThreadReceiverRepository>().Add(messageThreadReceiver);
                }

                _uow.Commit();

                return RedirectToAction("Index");
            }

            return View(createModel);
        }


        // GET: MemberArea/Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageThread messageThread = _uow.GetRepository<IMessageThreadRepository>()
                .GetUserThread((int)id, Convert.ToInt32(User.Identity.GetUserId()));

            if (messageThread == null)
            {
                return HttpNotFound();
            }
            return View(messageThread);
        }

        // POST: MemberArea/Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            
            List<Message> messages =_uow.GetRepository<IMessageRepository>()
                .GetAllByThreadIdAndUserId(id, userId);

         
            _uow.Commit();
            return RedirectToAction("Index");
        }
    }
}
