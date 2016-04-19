using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces;
using Domain;

namespace Web.Areas.MemberArea.Controllers
{
    [Authorize(Roles = "CarOwner")]
    public class BlogPostsController : Controller
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IUOW _uow;

        public BlogPostsController(IUOW uow)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
        }

        // GET: MemberArea/BlogPosts
        public ActionResult Index()
        {
            var blogPosts = _uow.GetRepository<IBlogPostRepository>().All;
            return View(blogPosts);
        }

        // GET: MemberArea/BlogPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = _uow.GetRepository<IBlogPostRepository>().GetById(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: MemberArea/BlogPosts/Create
        public ActionResult Create(int? blogId)
        {
            ViewBag.BlogId = blogId; // FIXME
            return View();
        }

        // POST: MemberArea/BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogPostId,BlogId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                _uow.GetRepository<IBlogPostRepository>().Add(blogPost);
                _uow.Commit();
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        // GET: MemberArea/BlogPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = _uow.GetRepository<IBlogPostRepository>().GetById(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: MemberArea/BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogPostId,BlogId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                _uow.GetRepository<IBlogPostRepository>().Update(blogPost);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: MemberArea/BlogPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = _uow.GetRepository<IBlogPostRepository>().GetById(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: MemberArea/BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.GetRepository<IBlogRepository>().Delete(id);
            _uow.Commit();
            return RedirectToAction("Index");
        }
    }
}
