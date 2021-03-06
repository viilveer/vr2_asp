﻿using System;
using System.Web.Mvc;
using Interfaces.Repositories;
using Interfaces.UOW;
using Microsoft.AspNet.Identity;
using BLL.ViewModels.Dashboard;
using Web.Controllers;

namespace Web.Areas.MemberArea.Controllers
{
    [Authorize(Roles = "User")]
    public class DashboardController : BaseController
    {

        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly BaseIUOW _uow;

        public DashboardController(BaseIUOW uow)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
        }

        // GET: MemberArea/Dashboard
        public ActionResult Index()
        {
            ViewModel vm = new ViewModel();
            vm.NewBlogPostList = _uow.GetRepository<IBlogPostRepository>().GetDashBoardNewestBlogPosts(10);
            vm.FavoriteBlogPostList = _uow.GetRepository<IBlogPostRepository>().GetDashBoardFavoriteBlogBlogPosts(User.Identity.GetUserId<int>(), 10);
            return View(vm);
        }
    }
}
