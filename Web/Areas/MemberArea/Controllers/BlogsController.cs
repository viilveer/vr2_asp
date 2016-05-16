using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain;
using Microsoft.AspNet.Identity;
using PagedList;
using Web.Areas.MemberArea.ViewModels.Blog;
using Web.Areas.MemberArea.ViewModels.BlogPost;
using Web.Controllers;
using DetailsModel = Web.Areas.MemberArea.ViewModels.Blog.DetailsModel;

namespace Web.Areas.MemberArea.Controllers
{
    [Authorize(Roles = "User")]
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
        public ActionResult Index(IndexModel vm)
        {
            int totalVehicleCount;
            string realSortProperty;

            // if not set, set base values
            vm.PageNumber = vm.PageNumber ?? 1;
            vm.PageSize = vm.PageSize ?? 25;

            var res = _uow.GetRepository<IBlogRepository>().GetList(Request.Params["blogName"], vm.SortProperty, vm.PageNumber.Value - 1, vm.PageSize.Value, out totalVehicleCount, out realSortProperty);

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
            Blog blog = _uow.GetRepository<IBlogRepository>().GetById(id);
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



        // GET: MemberArea/Blog/ConnectTo/5
        public ActionResult ConnectTo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int blogId = id.Value;
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            
            UserBlogConnection userBlogConnection =
                _uow.GetRepository<IUserBlogConnectionRepository>()
                    .GetUserAndBlogConnection(userId, blogId);

            if (userBlogConnection == null) // TODO:: handle count > 1?
            {
                UserBlogConnection connection = new UserBlogConnection()
                {
                    UserId = userId,
                    BlogId = blogId,
                };
                _uow.GetRepository<IUserBlogConnectionRepository>().Add(connection);
                _uow.Commit();
            }

            return RedirectToAction("Details", "Blogs", new { area = "MemberArea", id = id });
        }

        // GET: MemberArea/Blog/ConnectTo/5
        public ActionResult DisconnectFrom(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int blogId = id.Value;
            int userId = User.Identity.GetUserId<int>();

            
            _uow.GetRepository<IUserBlogConnectionRepository>().DeleteByUserIdAndBlogId(userId, blogId);
            _uow.Commit();

            return RedirectToAction("Details", "Blogs", new { area = "MemberArea", id = id });
        }


    }
}
