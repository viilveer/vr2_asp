using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class BlogRepository : EFRepository<Blog>, IBlogRepository
    {
        public BlogRepository(IDbContext dbContext) : base(dbContext)
        {

        }

        public Blog GetOneByUserAndId(int id, int userId)
        {
            return DbSet.Single(o => o.AuthorId == userId && o.BlogId == id);
        }

        public IEnumerable<Blog> GetListByUserId(int userId, string sortProperty, int pageNumber, int pageSize, out int totalBlogCount, out string realSortProperty)
        {
            sortProperty = sortProperty?.ToLower() ?? "";


            //start building up the query
            var res = DbSet
                .Where(p => p.Vehicle.UserId == userId).Include(p => p.Vehicle);


            // set up sorting
            switch (sortProperty)
            {
                default:
                case "_make":
                    res = res
                        .OrderBy(o => o.Name).ThenBy(o => o.Name);
                    realSortProperty = "_make";
                    break;
            }

            totalBlogCount = res.Count();

            var reslist = res
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return reslist;
        }
    }
}
