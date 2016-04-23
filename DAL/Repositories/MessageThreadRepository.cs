using System;
using System.Collections.Generic;
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
            return DbSet.Where(u => u.SenderId == userId && u.ReceiverId == userId).ToList();
        }

        public MessageThread GetUserThread(int threadId, int userId)
        {
            return DbSet.Single(u => u.MessageThreadId == threadId && (u.ReceiverId == userId || u.SenderId == userId));
        }

        // Use in SENT folder (if exists)
        public List<MessageThread> GetAllBySenderId(int userId)
        {
            return DbSet.Where(u => u.SenderId == userId).ToList();
        }

        // Use in RECEIVED folder (if exists)
        public List<MessageThread> GetAllByReceiverId(int userId)
        {
            return DbSet.Where(u => u.ReceiverId == userId).ToList();
        }
    }
}
