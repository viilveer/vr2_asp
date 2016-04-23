using System;
using System.Web.Mvc;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Web.Areas.MemberArea.ViewModels.Dashboard;
using Web.Controllers;

namespace Web.Areas.MemberArea.Controllers
{
    [Authorize(Roles = "User")]
    public class DashboardController : BaseController
    {

        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IUOW _uow;

        public DashboardController(IUOW uow)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
        }

        // GET: MemberArea/Dashboard
        public ActionResult Index()
        {
            ViewModel vm = new ViewModel();
            vm.VehicleCount =_uow.GetRepository<IVehicleRepository>().CountByUserId(Convert.ToInt32(User.Identity.GetUserId()));
            return View(vm);
        }
    }
}
