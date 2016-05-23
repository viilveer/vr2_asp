using Domain;

namespace Interfaces.Repositories
{
    public interface IUserBlogConnectionRepository : IBaseRepository<UserBlogConnection>
    {
        UserBlogConnection GetUserAndBlogConnection(int userId, int blogId);

        void DeleteByUserIdAndBlogId(int userId, int blogId);
        void DeleteByBlogId(int blogId);
    }
}
