using System;
using System.Net;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain;
using Domain.Identity;
using Microsoft.AspNet.Identity;
using Web.Areas.MemberArea.ViewModels.Blog;
using Web.Controllers;

namespace Web.Areas.MemberArea.Controllers
{
    [Authorize(Roles = "CarOwner")]
    public class BlogsController : BaseController
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IUOW _uow;

        public BlogsController(IUOW uow)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
        }

        // GET: MemberArea/Blogs
        public ActionResult Index()
        {
            var blogs = _uow.GetRepository<IBlogRepository>().All;
            return View(blogs);
        }

        // GET: MemberArea/Blogs/Details/5
        //TODO :: add blogposting here
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = _uow.GetRepository<IBlogRepository>().GetById(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }


        // GET: MemberArea/Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = _uow.GetRepository<IBlogRepository>().GetById(id);
            if (blog == null)
            {
                return HttpNotFound();
            }

            UpdateModel updateModel = new UpdateModel()
            {
                HeadLine = blog.HeadLine?.Value,
                Name = blog.Name?.Value,
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
            Blog blog = _uow.GetRepository<IBlogRepository>().GetById(id); // TODO :: write custom repo query (include user)
            if (blog == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(User.Identity.GetUserId());
                UserInt user = _uow.GetRepository<IUserIntRepository>().GetById(userId);
                blog = updateModel.UpdateBlog(blog, user);
                _uow.GetRepository<IBlogRepository>().Update(blog);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            return View(updateModel);
        }

     
    }
}
