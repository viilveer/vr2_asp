using System.Collections.Generic;
using System.Linq;
using API_DAL.Interfaces;
using Domain;

namespace API_DAL.Repositories
{
    public class MessageRepository : ApiRepository<Message>, IMessageRepository
    {
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
