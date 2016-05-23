using System.Collections.Generic;
using Domain;

namespace Interfaces.Repositories
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        List<Message> GetAllByThreadId(int threadId);
        List<Message> GetAllByThreadIdAndUserId(int threadId, int userId);
    }
}
