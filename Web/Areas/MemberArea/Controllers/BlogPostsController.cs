using System;
using System.Net;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain;
using Microsoft.AspNet.Identity;
using Web.Areas.MemberArea.ViewModels.BlogPost;

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

        // GET: MemberArea/BlogPosts/Edit/5
        public ActionResult Create(int? id)
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

            return View(new CreateModel());
        }

        // POST: MemberArea/BlogPosts/Create/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Message, Title")] CreateModel model, int? id)
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
                _uow.GetRepository<IBlogPostRepository>().Add(model.GetBlogPost(blog, User.Identity.GetUserId<int>()));
                _uow.Commit();
                return RedirectToAction("Edit", "BlogPosts", new { area = "MemberArea", id = id.Value });
            }
            return View(model);
        }


        // GET: MemberArea/BlogPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = _uow.GetRepository<IBlogPostRepository>().GetOneByUserAndId(id.Value, User.Identity.GetUserId<int>());
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            
            return View(UpdateModelFactory.CreateFromBlogPost(blogPost));
        }

        // POST: MemberArea/BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Message, Title")] UpdateModel model, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = _uow.GetRepository<IBlogPostRepository>().GetOneByUserAndId(id.Value, User.Identity.GetUserId<int>());
            if (blogPost == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                blogPost = model.UpdateBlogPost(blogPost);

                _uow.GetRepository<IBlogPostRepository>().Update(blogPost);
                _uow.Commit();
                return RedirectToAction("Edit", new {id = id.Value});
            }
            return View(model);
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
            BlogPost blogPost = _uow.GetRepository<IBlogPostRepository>().GetOneByUserAndId(id, User.Identity.GetUserId<int>());

            if (blogPost == null)
            {
                return HttpNotFound();
            }

            _uow.GetRepository<IBlogPostRepository>().Delete(id);
            _uow.Commit();
            return RedirectToAction("Details", "MyBlogs", new {id = blogPost.BlogId});
        }
    }
}
