using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_DAL.Interfaces;
using Domain;

namespace API_DAL.Repositories
{
    public class MessageThreadRepository : ApiRepository<MessageThread>, IMessageThreadRepository
    {
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
