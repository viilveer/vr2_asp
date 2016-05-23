using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using Domain;
using Interfaces.Repositories;
using Microsoft.Owin.Security;

namespace API_DAL.Repositories
{
    public class BlogPostRepository : ApiRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public BlogPost GetOneByUserAndId(int id, int userId)
        {
            throw new System.NotImplementedException();
        }

        public List<BlogPost> GetAllByBlogId(int blogId, string sortProperty, int pageNumber, int pageSize, out int totalItemCount,
            out string realSortProperty)
        {
            throw new System.NotImplementedException();
        }

        public List<BlogPost> GetDashBoardFavoriteBlogBlogPosts(int userId, int limit)
        {
            throw new System.NotImplementedException();
        }

        public List<BlogPost> GetDashBoardNewestBlogPosts(int limit)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteByBlogId(int blogId)
        {
            throw new System.NotImplementedException();
        }
    }
}
