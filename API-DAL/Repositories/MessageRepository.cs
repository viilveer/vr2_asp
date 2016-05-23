using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Interfaces.Repositories;
using Domain;
using Microsoft.Owin.Security;

namespace API_DAL.Repositories
{
    public class MessageRepository : ApiRepository<Message>, IMessageRepository
    {
        public MessageRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
        public List<Message> GetAllByThreadId(int threadId)
        {
            throw new System.NotImplementedException();
        }

        public List<Message> GetAllByThreadIdAndUserId(int threadId, int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
