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

        public List<MessageThread> GetUserThreads(int userId, string sortProperty, int pageNumber, int pageSize, out int totalThreadCount, out string realSortProperty)
        {
            sortProperty = sortProperty?.ToLower() ?? "";
            realSortProperty = sortProperty;


            var res =
                DbSet.Where(m => m.MessageThreadReceivers.Any(x => x.UserId == userId))
                    .Include(u => u.MessageThreadReceivers);


            // set up sorting
            switch (sortProperty)
            {
                default:
                case "_createdAt":
                    res = res
                        .OrderBy(o => o.CreatedAt);
                    realSortProperty = "_createdAt";
                    break;
            }

            totalThreadCount = res.Count();

            var reslist = res
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return reslist;
        }

        public MessageThread GetUserThread(int threadId, int userId)
        {
            return DbSet.Where(u => u.MessageThreadId == threadId && u.MessageThreadReceivers.Any(x => x.UserId == userId)).Include(u => u.MessageThreadReceivers).Single();
        }

    }
}
