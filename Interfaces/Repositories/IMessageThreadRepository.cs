using System.Collections.Generic;
using Domain;

namespace Interfaces.Repositories
{
    public interface IMessageThreadRepository : IBaseRepository<MessageThread>
    {
        List<MessageThread> GetUserThreads(int userId, string sortProperty, int pageNumber, int pageSize, out int totalThreadCount, out string realSortProperty);
        MessageThread GetUserThread(int threadId, int userId);
     
    }
}
