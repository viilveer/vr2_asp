using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain;
using Domain.Identity;
using Interfaces.Repositories;
using Interfaces.UOW;
using Web.Areas.MemberArea.ViewModels.Profile;

namespace Web.Areas.MemberArea.Controllers
{
    [Authorize(Roles = "User")]
    public class ProfileController : Controller
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly BaseIUOW _uow;
        private ApplicationUserManager _userManager;

        public ProfileController(BaseIUOW uow, ApplicationUserManager userManager)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
            _userManager = userManager;
        }

        // GET: MemberArea/Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInt user = _uow.GetRepository<IUserIntRepository>().GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(DetailsModelFactory.CreateFromUserInt(user));
        }

    }
}
