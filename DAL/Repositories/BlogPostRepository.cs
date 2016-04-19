using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class BlogPostRepository : EFRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
