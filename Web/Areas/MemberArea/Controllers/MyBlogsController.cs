using System;
using System.Net;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain;
using Microsoft.AspNet.Identity;
using PagedList;
using Santhos.Web.Mvc.BootstrapFlashMessages;
using Web.Areas.MemberArea.ViewModels.Blog;
using Web.Controllers;
using DetailsModel = Web.Areas.MemberArea.ViewModels.Blog.DetailsModel;
using UpdateModel = Web.Areas.MemberArea.ViewModels.Blog.UpdateModel;

namespace Web.Areas.MemberArea.Controllers
{
    [Authorize(Roles = "CarOwner")]
    public class MyBlogsController : BaseController
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IUOW _uow;

        public MyBlogsController(IUOW uow)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
        }

        // GET: MemberArea/Blogs
        public ActionResult Index(IndexModel vm)
        {
            int totalVehicleCount;
            string realSortProperty;

            // if not set, set base values
            vm.PageNumber = vm.PageNumber ?? 1;
            vm.PageSize = vm.PageSize ?? 25;

            var res = _uow.GetRepository<IBlogRepository>().GetListByUserId(User.Identity.GetUserId<int>(), vm.SortProperty, vm.PageNumber.Value - 1, vm.PageSize.Value, out totalVehicleCount, out realSortProperty);

            vm.SortProperty = realSortProperty;

            // https://github.com/kpi-ua/X.PagedList
            vm.Blogs = new StaticPagedList<Blog>(res, vm.PageNumber.Value, vm.PageSize.Value, totalVehicleCount);

            return View(vm);
        }

        // GET: MemberArea/Blogs/Details/5
        public ActionResult Details(int? id, DetailsModel detailsModel)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = _uow.GetRepository<IBlogRepository>().GetOneByUserAndId(id.Value, User.Identity.GetUserId<int>());
            if (blog == null)
            {
                return HttpNotFound();
            }

            int totalItemCount;
            string realSortProperty;

            // if not set, set base values
            detailsModel.PageNumber = detailsModel.PageNumber ?? 1;
            detailsModel.PageSize = detailsModel.PageSize ?? 25;
            detailsModel.HeadLine = blog.HeadLine;
            detailsModel.BlogId = blog.BlogId;
            detailsModel.Name = blog.Name;

            var res = _uow.GetRepository<IBlogPostRepository>().GetAllByBlogId(id.Value, detailsModel.SortProperty, detailsModel.PageNumber.Value - 1, detailsModel.PageSize.Value, out totalItemCount, out realSortProperty);

            detailsModel.SortProperty = realSortProperty;

            // https://github.com/kpi-ua/X.PagedList
            detailsModel.BlogPosts = new StaticPagedList<BlogPost>(res, detailsModel.PageNumber.Value, detailsModel.PageSize.Value, totalItemCount);

            return View(detailsModel);
        }



        // GET: MemberArea/Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = _uow.GetRepository<IBlogRepository>().GetOneByUserAndId(id.Value, User.Identity.GetUserId<int>());
            if (blog == null)
            {
                return HttpNotFound();
            }

            UpdateModel updateModel = new UpdateModel()
            {
                HeadLine = blog.HeadLine,
                Name = blog.Name,
                VehicleName = blog.Vehicle.Make + " " + blog.Vehicle.Model
            };


            return View(updateModel);
        }

        // POST: MemberArea/Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Edit([Bind(Include = "Name, HeadLine")] UpdateModel updateModel, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = _uow.GetRepository<IBlogRepository>().GetOneByUserAndId(id.Value, User.Identity.GetUserId<int>());
            if (blog == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                blog = updateModel.UpdateBlog(blog);
                _uow.GetRepository<IBlogRepository>().Update(blog);
                _uow.Commit();
                this.FlashSuccess("Blog post edited");
                return RedirectToAction("Index");
            }
            this.FlashDanger("There were errors on form");
            return View(updateModel);
        }

    }
}
