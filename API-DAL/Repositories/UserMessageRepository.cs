using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class UserMessageRepository : EFRepository<UserMessage>, IUserMessageRepository
    {
        public UserMessageRepository(IDbContext dbContext) : base(dbContext)
        {
        }

    }
}
