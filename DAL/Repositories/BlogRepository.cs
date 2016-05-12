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

        public IEnumerable<Blog> GetListByUserId(int userId, string sortProperty, int pageNumber, int pageSize, out int totalBlogCount, out string realSortProperty)
        {
            sortProperty = sortProperty?.ToLower() ?? "";
            realSortProperty = sortProperty;


            //start building up the query
            var res = DbSet
                .Where(p => p.Vehicle.UserId == userId).Include(p => p.Vehicle);


            // set up sorting
            switch (sortProperty)
            {
                case "_make":
                    res = res
                        .OrderBy(o => o.Vehicle.Make).ThenBy(o => o.Vehicle.Make);
                    break;

                default:
                case "_name":
                    res = res
                        .OrderBy(o => o.Name).ThenBy(o => o.Name);
                    realSortProperty = "title";
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
