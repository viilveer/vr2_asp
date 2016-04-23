using System.Collections.Generic;
using Domain;

namespace DAL.Interfaces
{
    public interface IMessageRepository : IEFRepository<Message>
    {
        List<Message> GetAllByThreadId(int threadId);
    }
}
