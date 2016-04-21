using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class BlogPostRepository : EFRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public List<BlogPost> GetAllByBlogId(int blogId)
        {
            return DbSet.Where(u => u.BlogId == blogId).ToList();
        }
    }
}
