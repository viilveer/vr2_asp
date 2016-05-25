using System;
using System.Web.Http;
using DAL.Interfaces;
using Domain.Identity;
using Interfaces.Repositories;

namespace WebAPI.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "User")]
    public class ProfileController : ApiController
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IUOW _uow;
        private ApplicationUserManager _userManager;

        public ProfileController(IUOW uow, ApplicationUserManager userManager)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
            _userManager = userManager;
        }

        // GET: MemberArea/Profile/Details/5
        public UserInt Details(int id)
        {
            return _uow.GetRepository<IUserIntRepository>().GetById(id);
        }

    }
}
