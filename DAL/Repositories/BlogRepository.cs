using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class BlogRepository : EFRepository<Blog>, IBlogRepository
    {
        public BlogRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
