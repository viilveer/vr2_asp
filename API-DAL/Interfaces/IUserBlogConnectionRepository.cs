using Domain;

namespace API_DAL.Interfaces
{
    public interface IUserBlogConnectionRepository : IApiRepository<UserBlogConnection>
    {
        UserBlogConnection GetUserAndBlogConnection(int userId, int blogId);

        void DeleteByUserIdAndBlogId(int userId, int blogId);
        void DeleteByBlogId(int blogId);
    }
}
