using System;
using System.Web.Mvc;
using Interfaces.Repositories;
using Interfaces.UOW;
using Microsoft.AspNet.Identity;
using WebAPI.ViewModels.Dashboard;

namespace WebAPI.Controllers
{
    public class DashboardController : Controller
    {

        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly BaseIUOW _uow;

        public DashboardController(BaseIUOW uow)
        {
            _uow = uow;
        }

        // GET: MemberArea/Dashboard
        public ViewModel Index()
        {
            return new ViewModel
            {
                //NewBlogPostList = _uow.GetRepository<IBlogPostRepository>().GetDashBoardNewestBlogPosts(10),
                //FavoriteBlogPostList =
                //    _uow.GetRepository<IBlogPostRepository>().GetDashBoardFavoriteBlogBlogPosts(User.Identity.GetUserId<int>(), 10)
            };
        }
    }
}
