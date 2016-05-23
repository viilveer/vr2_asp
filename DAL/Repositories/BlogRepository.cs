using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using Domain;
using Interfaces.Repositories;

namespace DAL.Repositories
{
    public class BlogRepository : EFRepository<Blog>, IBlogRepository
    {
        public BlogRepository(IDbContext dbContext) : base(dbContext)
        {

        }

        public Blog GetOneByUserAndId(int id, int userId)
        {
            return DbSet.Where(o => o.AuthorId == userId && o.BlogId == id).Include(o => o.Vehicle).FirstOrDefault();
        }

        public IEnumerable<Blog> GetListByUserId(int userId, string sortProperty, int pageNumber, int pageSize, out int totalItemCount, out string realSortProperty)
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

            totalItemCount = res.Count();

            var reslist = res
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return reslist;
        }
        // TODO :: reduce WET
        public IEnumerable<Blog> GetList(String filter, string sortProperty, int pageNumber, int pageSize, out int totalItemCount,
            out string realSortProperty)
        {
            sortProperty = sortProperty?.ToLower() ?? "";


            //start building up the query
            var res = DbSet.Include(p => p.Vehicle);

            if (filter != null)
            {
                res = res.Where(x => x.Name.Contains(filter));
            }


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

            totalItemCount = res.Count();

            var reslist = res
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return reslist;
        }
    }
}
