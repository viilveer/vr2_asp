using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain;
using Domain.Identity;
using Microsoft.AspNet.Identity;
using Web.Areas.MemberArea.ViewModels.Message;
using Web.Areas.MemberArea.ViewModels.MessageThread;
using Web.Helpers;
using Web.Helpers.Factories;
using CreateModel = Web.Areas.MemberArea.ViewModels.Message.CreateModel;

namespace Web.Areas.MemberArea.Controllers
{
    public class MessagesController : Controller
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IUOW _uow;

        public MessagesController(IUOW uow)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
        }

        // GET: MemberArea/Messages
        public ActionResult Index()
        {
            List<MessageThread> messageThreads =
                _uow.GetRepository<IMessageThreadRepository>()
                    .GetAllUserThreads(Convert.ToInt32(User.Identity.GetUserId()));

            return View(messageThreads);
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
                .GetUserThread((int)id, Convert.ToInt32(User.Identity.GetUserId()));

            if (messageThread == null)
            {
                return HttpNotFound();
            }


            ViewModels.MessageThread.DetailsModel detailsModel = new ViewModels.MessageThread.DetailsModel()
            {
                Author = messageThread.Author.Email, // TODO :: fix
                Title = messageThread.Title.Value,
                NewMessageModel = new CreateModel(),
                DetailsModels = _uow.GetRepository<IMessageRepository>()
                    .GetAllByThreadId(messageThread.MessageThreadId)
                    .Select(DetailsModelFactory.CreateFromMessage)
                    .ToList(),
            };

            return View(detailsModel);
        }

        // TODO :: finish
        // GET: MemberArea/Messages/Details/5
        [HttpPost]
        public ActionResult Details(int? id, ViewModels.MessageThread.DetailsModel detailsModel)
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
                _uow.GetRepository<IMessageRepository>().Add(message);
                _uow.Commit();
                return RedirectToAction("Details", new {id = messageThread.MessageThreadId});
            }


            detailsModel.Author = messageThread.Author.Email; // TODO :: fix
            detailsModel.Title = messageThread.Title.Value;
            detailsModel.DetailsModels = _uow.GetRepository<IMessageRepository>()
                .GetAllByThreadId(messageThread.MessageThreadId)
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

            ViewModels.MessageThread.CreateModel createModel = new ViewModels.MessageThread.CreateModel();
            return View(createModel);
        }

        // POST: MemberArea/Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Text")] ViewModels.MessageThread.CreateModel createModel, int? receiverId)
        {
            UserInt user = _uow.GetRepository<IUserIntRepository>().GetById(receiverId);
            if (user == null)
            {
                return HttpNotFound();
            }


            if (ModelState.IsValid)
            {

                UserInt sender = UserIntFactory.CreateFromIdentity(_uow, User);
                NewThreadModel newThreadModel = new NewThreadModel(sender, user);
                newThreadModel.Prepare(createModel.Title, createModel.Text);

                _uow.GetRepository<IMessageThreadRepository>().Add(newThreadModel.MessageThread);

                newThreadModel.Message.MessageThreadId = newThreadModel.MessageThread.MessageThreadId;
                _uow.GetRepository<IMessageRepository>().Add(newThreadModel.Message);

                foreach (var messageReceiver in newThreadModel.MessageReceivers)
                {
                    messageReceiver.MessageId = newThreadModel.Message.MessageId;
                    _uow.GetRepository<IMessageReceiverRepository>().Add(messageReceiver);
                }

                foreach (var messageThreadReceiver in newThreadModel.MessageThreadReceivers)
                {
                    messageThreadReceiver.MessageThreadId = newThreadModel.Message.MessageId;
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
