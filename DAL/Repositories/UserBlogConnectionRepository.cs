using System.Linq;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class UserBlogConnectionRepository : EFRepository<UserBlogConnection>, IUserBlogConnectionRepository
    {
        public UserBlogConnectionRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public UserBlogConnection GetUserAndBlogConnection(int userId, int blogId)
        {
            return DbSet.Single(u => u.UserId == userId && u.BlogId == blogId);
        }

        public void DeleteByUserIdAndBlogId(int userId, int blogId)
        {
            var entity = GetUserAndBlogConnection(userId, blogId);
            if (entity == null) return;
            Delete(entity);
        }
    }
}
