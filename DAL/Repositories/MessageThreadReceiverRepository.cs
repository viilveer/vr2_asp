using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using Interfaces.Repositories;
using Domain;

namespace DAL.Repositories
{
    public class MessageThreadReceiverRepository : EFRepository<MessageThreadReceiver>, IMessageThreadReceiverRepository
    {
        public MessageThreadReceiverRepository(IDbContext dbContext) : base(dbContext)
        {
        }

    }
}
