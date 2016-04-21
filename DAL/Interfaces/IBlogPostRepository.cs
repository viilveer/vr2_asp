using System.Collections.Generic;
using Domain;

namespace DAL.Interfaces
{
    public interface IBlogPostRepository : IEFRepository<BlogPost>
    {
        List<BlogPost> GetAllByBlogId(int blogId);
    }
}
