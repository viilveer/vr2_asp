using System;
using System.Net.Http;
using Domain;
using Interfaces.Repositories;
using Microsoft.Owin.Security;
using NLog;

namespace API_DAL.Repositories
{
    public class UserBlogConnectionRepository : ApiRepository<UserBlogConnection>, IUserBlogConnectionRepository
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public UserBlogConnectionRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
        public UserBlogConnection GetUserAndBlogConnection(int userId, int blogId)
        {
            var response = HttpClient.GetAsync(EndPoint + $"{blogId}/Connect").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<UserBlogConnection>().Result;
                return res;
            }

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new UserBlogConnection();
        }

        public void DeleteByUserIdAndBlogId(int userId, int blogId)
        {
            var response = HttpClient.GetAsync(EndPoint + $"{blogId}/Disconnect").Result;
            if (!response.IsSuccessStatusCode)
            {
                _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                throw new Exception("Invalid call");
            }
        }

        public void DeleteByBlogId(int blogId)
        {
            throw new NotImplementedException();
        }
    }
}
