using System;
using System.Web.Mvc;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using WebAPI.ViewModels.Dashboard;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "User")]
    public class DashboardController : Controller
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
        public ViewModel Index()
        {
            return new ViewModel
            {
                NewBlogPostList = _uow.GetRepository<IBlogPostRepository>().GetDashBoardNewestBlogPosts(10),
                FavoriteBlogPostList =
                    _uow.GetRepository<IBlogPostRepository>()
                        .GetDashBoardFavoriteBlogBlogPosts(User.Identity.GetUserId<int>(), 10)
            };
        }
    }
}
