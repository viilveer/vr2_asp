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
using Web.Helpers.Factories;

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
                Sender = messageThread.Sender.Email, // TODO :: fix
                Receiver = messageThread.Receiver.Email, // TODO :: fix
                Title = messageThread.Title.Value,
                NewMessageModel = new CreateModel(),
                DetailsModels = _uow.GetRepository<IMessageRepository>()
                    .GetAllByThreadId(messageThread.MessageThreadId)
                    .Select(DetailsModelFactory.CreateFromMessage)
                    .ToList(),
            };

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


            if (ModelState.IsValid) // TODO :: transaction
            {
                UserInt sender = UserIntFactory.CreateFromIdentity(_uow, User);
                MessageThread thread = createModel.GetMessageThread(sender, user);
                _uow.GetRepository<IMessageThreadRepository>().Add(thread);
                

                Message message = createModel.GetMessage(sender, user);
                message.MessageThreadId = thread.MessageThreadId;
                _uow.GetRepository<IMessageRepository>().Add(message);

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
            _uow.GetRepository<IMessageThreadRepository>().Delete(id); // TODO :: delete with messages and check for user
            _uow.Commit();
            return RedirectToAction("Index");
        }
    }
}
