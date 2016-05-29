using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL;
using DAL.Interfaces;
using Domain;
using Interfaces.Repositories;
using Microsoft.AspNet.Identity;
using NLog;

namespace WebAPI.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "User"), RoutePrefix("api/Blogs")]
    public class BlogsController : ApiController
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IUOW _uow;
        
        public BlogsController(IUOW uow)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
        }
        /// <summary>
        /// Searches blog post. Can be accessed also as guest member
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public HttpResponseMessage Index(string blogName, string sortProperty, int pageNumber, int pageSize)
        {
            int totalCount;
            string realSortProperty;
           
            IEnumerable<Blog> blogs = _uow.GetRepository<IBlogRepository>().GetList(blogName, sortProperty, pageNumber, pageSize, out totalCount, out realSortProperty);
            var response = Request.CreateResponse(HttpStatusCode.OK, blogs);

            // Set headers for paging
            response.Headers.Add("X-Paging-PageNo", pageNumber.ToString());
            response.Headers.Add("X-Paging-PageSize", pageSize.ToString());
            response.Headers.Add("X-RealSortProperty", realSortProperty);
            response.Headers.Add("X-Paging-TotalRecordCount", totalCount.ToString());

            return response;
        }
        /// <summary>
        /// Paginated user blogs
        /// </summary>
        [HttpGet]
        [Route("User/Me/All")]
        public HttpResponseMessage MyBlogs(string sortProperty, int pageNumber, int pageSize)
        {
            int totalCount;
            string realSortProperty;

            IEnumerable<Blog> blogs = _uow.GetRepository<IBlogRepository>().GetListByUserId(User.Identity.GetUserId<int>(), sortProperty, pageNumber, pageSize, out totalCount, out realSortProperty);
            var response = Request.CreateResponse(HttpStatusCode.OK, blogs);

            // Set headers for paging
            response.Headers.Add("X-Paging-PageNo", pageNumber.ToString());
            response.Headers.Add("X-Paging-PageSize", pageSize.ToString());
            response.Headers.Add("X-RealSortProperty", realSortProperty);
            response.Headers.Add("X-Paging-TotalRecordCount", totalCount.ToString());

            return response;
        }
        /// <summary>
        /// Single user blog post
        /// </summary>
        [HttpGet]
        [Route("User/Me/{blogId}")]
        public Blog UserBlog(int blogId)
        {
            return _uow.GetRepository<IBlogRepository>().GetOneByUserAndId(blogId, User.Identity.GetUserId<int>());
        }
        /// <summary>
        /// Single blog post
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public Blog Index(int id)
        {
            return _uow.GetRepository<IBlogRepository>().GetById(id);
        }

        /// <summary>
        /// Creates new blog. 
        /// </summary>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Index(Blog blog)
        {
            blog.Vehicle = _uow.GetRepository<IVehicleRepository>()
                .GetByIdAndUserId(blog.VehicleId, User.Identity.GetUserId<int>());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _uow.GetRepository<IBlogRepository>().Add(blog);
            _uow.Commit();
            return Ok(blog);
        }
        /// <summary>
        /// Updates blog post
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update(int id, Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _uow.GetRepository<IBlogRepository>().Update(blog);
            _uow.Commit();


            return Ok();
        }

        /// <summary>
        /// Marks blog as a user favorite
        /// </summary>
        [HttpGet]
        [Route("{blogId}/Connect")]
        // GET: MemberArea/Blog/ConnectTo/5
        public IHttpActionResult ConnectTo(int blogId)
        {
            int userId = Convert.ToInt32(User.Identity.GetUserId());

            UserBlogConnection userBlogConnection =
                _uow.GetRepository<IUserBlogConnectionRepository>()
                    .GetUserAndBlogConnection(userId, blogId);

            if (userBlogConnection == null) 
            {
                UserBlogConnection connection = new UserBlogConnection
                {
                    UserId = userId,
                    BlogId = blogId
                };
                _uow.GetRepository<IUserBlogConnectionRepository>().Add(connection);
                _uow.Commit();
                return Ok(connection);
            }
            return NotFound();
        }
        /// <summary>
        /// Removes blog from user favorite blogs
        /// </summary>
        [HttpGet]
        [Route("{blogId}/Disconnect")]
        // GET: MemberArea/Blog/ConnectTo/5
        public IHttpActionResult DisconnectFrom(int blogId)
        {
            int userId = User.Identity.GetUserId<int>();
            _uow.GetRepository<IUserBlogConnectionRepository>().DeleteByUserIdAndBlogId(userId, blogId);
            _uow.Commit();
            return Ok();
        }
    }
}
