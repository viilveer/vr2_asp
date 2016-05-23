using System.Linq;
using System.Net.Http;
using Interfaces.Repositories;
using Domain;
using Microsoft.Owin.Security;

namespace API_DAL.Repositories
{
    public class UserBlogConnectionRepository : ApiRepository<UserBlogConnection>, IUserBlogConnectionRepository
    {
        public UserBlogConnectionRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
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
