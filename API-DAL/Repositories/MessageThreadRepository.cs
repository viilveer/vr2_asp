using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;
using Domain;
using Microsoft.Owin.Security;
using NLog;

namespace API_DAL.Repositories
{
    public class MessageThreadRepository : ApiRepository<MessageThread>, IMessageThreadRepository
    {
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        public MessageThreadRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
        public List<MessageThread> GetUserThreads(int userId, string sortProperty, int pageNumber, int pageSize, out int totalThreadCount,
            out string realSortProperty)
        {
            string requestParams =
                $"sortProperty={sortProperty}&pageNumber={pageNumber}&pageSize={pageSize}";

            var response = HttpClient.GetAsync(EndPoint + "Mine/?" + requestParams).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<MessageThread>>().Result;
                realSortProperty = response.Headers.GetValues("X-RealSortProperty").ToString();
                totalThreadCount = Convert.ToInt32(response.Headers.GetValues("X-Paging-TotalRecordCount").FirstOrDefault());
                return res;
            }
            realSortProperty = "";
            totalThreadCount = 0;

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new List<MessageThread>();
        }

        public MessageThread GetUserThread(int threadId, int userId)
        {
            var response = HttpClient.GetAsync(EndPoint + $"Mine/{threadId}").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<MessageThread>().Result;
                return res;
            }

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new MessageThread();
        }
    }
}
