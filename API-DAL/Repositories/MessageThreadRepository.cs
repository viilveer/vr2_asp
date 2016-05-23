using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;
using Domain;
using Microsoft.Owin.Security;

namespace API_DAL.Repositories
{
    public class MessageThreadRepository : ApiRepository<MessageThread>, IMessageThreadRepository
    {
        public MessageThreadRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
        public List<MessageThread> GetUserThreads(int userId, string sortProperty, int pageNumber, int pageSize, out int totalThreadCount,
            out string realSortProperty)
        {
            throw new NotImplementedException();
        }

        public MessageThread GetUserThread(int threadId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
