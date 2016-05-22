using System.Collections.Generic;
using Domain;

namespace API_DAL.Interfaces
{
    public interface IMessageRepository : IApiRepository<Message>
    {
        List<Message> GetAllByThreadId(int threadId);
        List<Message> GetAllByThreadIdAndUserId(int threadId, int userId);
    }
}
