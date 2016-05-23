using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using Interfaces.Repositories;
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
            return DbSet.Where(u => u.MessageThreadId == threadId).ToList();
        }

        public List<Message> GetAllByThreadIdAndUserId(int threadId, int userId)
        {
            return DbSet.Where(u => u.MessageThreadId == threadId).ToList();
        }
    }
}
