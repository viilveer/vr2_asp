using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class MessageThreadRepository : EFRepository<MessageThread>, IMessageThreadRepository
    {
        public MessageThreadRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public List<MessageThread> GetAllUserThreads(int userId)
        {
            return DbSet.Where(m => m.MessageThreadReceivers.Any(x => x.UserId==userId)).Include(u => u.MessageThreadReceivers).ToList();
        }

        public MessageThread GetUserThread(int threadId, int userId)
        {
            return DbSet.Where(u => u.MessageThreadId == threadId && u.MessageThreadReceivers.Any(x => x.UserId == userId)).Include(u => u.MessageThreadReceivers).Single();
        }

    }
}
