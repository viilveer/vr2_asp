using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain;
using Domain.Identity;
using Microsoft.AspNet.Identity;
using Web.Areas.MemberArea.ViewModels.BlogPost;
using Web.Controllers;
using DetailsModel = Web.Areas.MemberArea.ViewModels.Blog.DetailsModel;
using UpdateModel = Web.Areas.MemberArea.ViewModels.Blog.UpdateModel;

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

            CreateModel createModel = new CreateModel();
            List<BlogPost> blogPosts = _uow.GetRepository<IBlogPostRepository>().GetAllByBlogId(blog.BlogId);

            DetailsModel detailsModel = new DetailsModel()
            {
                HeadLine = blog.HeadLine.Value,
                BlogId = blog.BlogId,
                Name = blog.Name.Value,
                CreateModel = createModel,
                BlogPosts = blogPosts.Select(DetailsModelFactory.CreateFromBlogPost).ToList()
            };
            return View(detailsModel);
        }

        // GET: MemberArea/Blogs/Details/5
        // TODO :: Fix WET code
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int? id, DetailsModel detailsModel)
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

            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(User.Identity.GetUserId());
                UserInt user = _uow.GetRepository<IUserIntRepository>().GetById(userId);

                BlogPost blogPost = detailsModel.CreateModel.GetBlogPost(blog, user);


                _uow.GetRepository<IBlogPostRepository>().Add(blogPost);
                _uow.Commit();
                return RedirectToAction("Edit", "BlogPosts",  new {id = blogPost.BlogPostId});
            }

            List<BlogPost> blogPosts = _uow.GetRepository<IBlogPostRepository>().GetAllByBlogId(blog.BlogId);

            detailsModel.HeadLine = blog.HeadLine.Value;
            detailsModel.BlogId = blog.BlogId;
            detailsModel.Name = blog.Name.Value;
            detailsModel.BlogPosts = blogPosts.Select(DetailsModelFactory.CreateFromBlogPost).ToList();

            return View(detailsModel);
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
