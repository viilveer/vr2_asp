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
            vm.NewBlogPostList =_uow.GetRepository<IBlogPostRepository>().GetDashBoardNewestBlogPosts(10);
            vm.FavoriteBlogPostList =_uow.GetRepository<IBlogPostRepository>().GetDashBoardFavoriteBlogBlogPosts(User.Identity.GetUserId<int>(), 10);
            return View(vm);
        }
    }
}
