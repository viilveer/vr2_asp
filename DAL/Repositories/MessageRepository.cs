using System.Collections.Generic;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class MessageRepository : EFRepository<Message>, IMessageRepository
    {
        public MessageRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public List<Message> GetAllByThreadId(int threadId)
        {
            throw new System.NotImplementedException();
        }
    }
}
