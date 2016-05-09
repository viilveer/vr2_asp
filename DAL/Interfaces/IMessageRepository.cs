using System.Collections.Generic;
using Domain;

namespace DAL.Interfaces
{
    public interface IMessageRepository : IEFRepository<Message>
    {
        List<Message> GetAllByThreadId(int threadId);
        List<Message> GetAllByThreadIdAndUserId(int threadId, int userId);
    }
}
