using System.Collections.Generic;
using Domain;

namespace API_DAL.Interfaces
{
    public interface IMessageThreadRepository : IApiRepository<MessageThread>
    {
        List<MessageThread> GetUserThreads(int userId, string sortProperty, int pageNumber, int pageSize, out int totalThreadCount, out string realSortProperty);
        MessageThread GetUserThread(int threadId, int userId);
     
    }
}
