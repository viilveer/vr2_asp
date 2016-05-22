using System.Linq;
using API_DAL.Interfaces;
using Domain;

namespace API_DAL.Repositories
{
    public class UserBlogConnectionRepository : ApiRepository<UserBlogConnection>, IUserBlogConnectionRepository
    {
        public UserBlogConnection GetUserAndBlogConnection(int userId, int blogId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteByUserIdAndBlogId(int userId, int blogId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteByBlogId(int blogId)
        {
            throw new System.NotImplementedException();
        }
    }
}
