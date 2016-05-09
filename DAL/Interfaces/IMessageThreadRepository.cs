using System.Collections.Generic;
using Domain;

namespace DAL.Interfaces
{
    public interface IMessageThreadRepository : IEFRepository<MessageThread>
    {
        List<MessageThread> GetAllUserThreads(int userId);
        MessageThread GetUserThread(int threadId, int userId);
     
    }
}
