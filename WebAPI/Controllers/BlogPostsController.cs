﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using DAL.Interfaces;
using Domain;
using Interfaces.Repositories;
using Microsoft.AspNet.Identity;

namespace WebAPI.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "CarOwner")]
    [RoutePrefix("api/BlogPosts")]
    public class BlogPostsController : ApiController
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IUOW _uow;

        public BlogPostsController(IUOW uow)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
        }
        /// <summary>
        /// Retrieves user favorited blog posts
        /// </summary>
        [HttpGet]
        [Route("GetUserBlogPosts/{limit}")]
        public List<BlogPost> GetUserBlogPosts(int limit = 10)
        {
            return
                _uow.GetRepository<IBlogPostRepository>()
                    .GetDashBoardFavoriteBlogBlogPosts(User.Identity.GetUserId<int>(), limit);
        }
        /// <summary>
        /// Retrieves new blog posts
        /// </summary>
        [HttpGet]
        [Route("GetNewBlogPosts/{limit}")]
        public List<BlogPost> GetNewBlogPosts(int limit = 10)
        {
            return _uow.GetRepository<IBlogPostRepository>().GetDashBoardNewestBlogPosts(limit);
        }

        /// <summary>
        /// Creates new blog post
        /// </summary>
        [HttpPost]
        public IHttpActionResult Create(BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _uow.GetRepository<IBlogPostRepository>().Add(blogPost);
            _uow.Commit();


            return Ok(blogPost);
        }
        /// <summary>
        /// Updates a blog post
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update(int id, BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _uow.GetRepository<IBlogPostRepository>().Update(blogPost);
            _uow.Commit();


            return Ok();
        }

        /// <summary>
        /// Lists all blog posts from certain blog (paginated). Can be accessed as guest
        /// </summary>
        [HttpGet]
        [Route("Blog/{id}/All")]
        [AllowAnonymous]
        public HttpResponseMessage Index(int id, string sortProperty, int pageNumber, int pageSize)
        {
            int totalCount;
            string realSortProperty;

            IEnumerable<BlogPost> blogs = _uow.GetRepository<IBlogPostRepository>().GetAllByBlogId(id, sortProperty, pageNumber, pageSize, out totalCount, out realSortProperty);
            var response = Request.CreateResponse(HttpStatusCode.OK, blogs);

            // Set headers for paging
            response.Headers.Add("X-Paging-PageNo", pageNumber.ToString());
            response.Headers.Add("X-Paging-PageSize", pageSize.ToString());
            response.Headers.Add("X-RealSortProperty", realSortProperty);
            response.Headers.Add("X-Paging-TotalRecordCount", totalCount.ToString());

            return response;
        }

        /// <summary>
        /// Fetches single blog post
        /// </summary>
        [HttpGet]
        [Route("User/Me/{blogPostId}")]
        public BlogPost UserBlog(int blogPostId)
        {
            return _uow.GetRepository<IBlogPostRepository>().GetOneByUserAndId(blogPostId, User.Identity.GetUserId<int>());
        }
        /// <summary>
        /// Deletes blog post
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            BlogPost blogPost = _uow.GetRepository<IBlogPostRepository>().GetOneByUserAndId(id, User.Identity.GetUserId<int>());
            if (blogPost != null)
            {
                _uow.GetRepository<IBlogPostRepository>().Delete(blogPost.BlogPostId);

                _uow.Commit();
                return Ok();
            }
            return BadRequest();

        }

        //// GET: MemberArea/BlogPosts/Edit/5
        //public ActionResult Create(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Blog blog = _uow.GetRepository<IBlogRepository>().GetOneByUserAndId(id.Value, User.Identity.GetUserId<int>());
        //    if (blog == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(new CreateModel());
        //}

        //// POST: MemberArea/BlogPosts/Create/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[System.Web.Mvc.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Message, Title")] CreateModel model, int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Blog blog = _uow.GetRepository<IBlogRepository>().GetOneByUserAndId(id.Value, User.Identity.GetUserId<int>());
        //    if (blog == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _uow.GetRepository<IBlogPostRepository>().Add(model.GetBlogPost(blog, User.Identity.GetUserId<int>()));
        //        _uow.Commit();
        //        return RedirectToAction("Edit", "BlogPosts", new { area = "MemberArea", id = id.Value });
        //    }
        //    return View(model);
        //}


        //// GET: MemberArea/BlogPosts/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BlogPost blogPost = _uow.GetRepository<IBlogPostRepository>().GetOneByUserAndId(id.Value, User.Identity.GetUserId<int>());
        //    if (blogPost == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(UpdateModelFactory.CreateFromBlogPost(blogPost));
        //}

        //// POST: MemberArea/BlogPosts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[System.Web.Mvc.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Message, Title")] UpdateModel model, int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BlogPost blogPost = _uow.GetRepository<IBlogPostRepository>().GetOneByUserAndId(id.Value, User.Identity.GetUserId<int>());
        //    if (blogPost == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        blogPost = model.UpdateBlogPost(blogPost);

        //        _uow.GetRepository<IBlogPostRepository>().Update(blogPost);
        //        _uow.Commit();
        //        return RedirectToAction("Edit", new { id = id.Value });
        //    }
        //    this.FlashDanger("There were errors on form-");
        //    return View(model);
        //}

        //// GET: MemberArea/BlogPosts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BlogPost blogPost = _uow.GetRepository<IBlogPostRepository>().GetById(id);
        //    if (blogPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(blogPost);
        //}

        //// POST: MemberArea/BlogPosts/Delete/5
        //[System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    BlogPost blogPost = _uow.GetRepository<IBlogPostRepository>().GetOneByUserAndId(id, User.Identity.GetUserId<int>());

        //    if (blogPost == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    _uow.GetRepository<IBlogPostRepository>().Delete(id);
        //    _uow.Commit();
        //    this.FlashSuccess("You successfully deleted a blogpost-");
        //    return RedirectToAction("Details", "MyBlogs", new { id = blogPost.BlogId });
        //}
    }
}
