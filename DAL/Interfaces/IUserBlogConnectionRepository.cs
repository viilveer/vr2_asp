using Domain;

namespace DAL.Interfaces
{
    public interface IUserBlogConnectionRepository : IEFRepository<UserBlogConnection>
    {
        UserBlogConnection GetUserAndBlogConnection(int userId, int blogId);

        void DeleteByUserIdAndBlogId(int userId, int blogId);
    }
}
