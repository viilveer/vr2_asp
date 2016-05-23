using System.Collections.Generic;
using Domain;

namespace Interfaces.Repositories
{
    public interface IBlogPostRepository : IBaseRepository<BlogPost>
    {
        BlogPost GetOneByUserAndId(int id, int userId);
        List<BlogPost> GetAllByBlogId(int blogId, string sortProperty, int pageNumber, int pageSize, out int totalItemCount, out string realSortProperty);
        List<BlogPost> GetDashBoardFavoriteBlogBlogPosts(int userId, int limit);
        List<BlogPost> GetDashBoardNewestBlogPosts(int limit);
        void DeleteByBlogId(int blogId);
    }
}
