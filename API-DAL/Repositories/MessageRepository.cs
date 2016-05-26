using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Interfaces.Repositories;
using Domain;
using Microsoft.Owin.Security;
using NLog;

namespace API_DAL.Repositories
{
    public class MessageRepository : ApiRepository<Message>, IMessageRepository
    {
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public MessageRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
        public List<Message> GetAllByThreadId(int threadId)
        {
            throw new System.NotImplementedException();
        }

        public List<Message> GetAllByThreadIdAndUserId(int threadId, int userId)
        {

            var response = HttpClient.GetAsync(EndPoint + $"{threadId}/User/Me/").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<Message>>().Result;
                return res;
            }

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new List<Message>();
        }
    }
}
