using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using DAL.Interfaces;
using Domain;
using Interfaces.Repositories;

namespace DAL.Repositories
{
    public class BlogPostRepository : EFRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public BlogPost GetOneByUserAndId(int id, int userId)
        {
            return DbSet.FirstOrDefault(o => o.AuthorId == userId && o.BlogPostId == id);
        }

        public List<BlogPost> GetDashBoardNewestBlogPosts(int limit)
        {
            return DbSet.OrderByDescending(u => u.CreatedAt).Include(u => u.Blog).Take(limit).ToList();
        }

        public void DeleteByBlogId(int blogId)
        {
            DbSet.RemoveRange(DbSet.Where(x => x.BlogId == blogId));
        }

        public List<BlogPost> GetDashBoardFavoriteBlogBlogPosts(int userId, int limit)
        {
            return DbSet
                .Where(u => u.Blog.UserBlogConnections.Any(x => x.UserId == userId))
                .OrderByDescending(u => u.CreatedAt)
                .Include("Blog")
                .Include("Blog.UserBlogConnections")
                .Take(limit)
                .ToList();

        }


        public List<BlogPost> GetAllByBlogId(int blogId, string sortProperty, int pageNumber, int pageSize, out int totalItemCount, out string realSortProperty)
        {
            sortProperty = sortProperty?.ToLower() ?? "";


            //start building up the query
            var res = DbSet.Where(u => u.BlogId == blogId);


            // set up sorting
            switch (sortProperty)
            {
                default:
                case "_title":
                    res = res
                        .OrderBy(o => o.Title).ThenBy(o => o.Title);
                    realSortProperty = "_title";
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
