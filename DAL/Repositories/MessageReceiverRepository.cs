using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class MessageReceiverRepository : EFRepository<MessageReceiver>, IMessageReceiverRepository
    {
        public MessageReceiverRepository(IDbContext dbContext) : base(dbContext)
        {
        }

    }
}
