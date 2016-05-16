using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Domain;

namespace DAL.Interfaces
{
    public interface IBlogRepository : IEFRepository<Blog>
    {
        Blog GetOneByUserAndId(int id, int userId);
        IEnumerable<Blog> GetListByUserId(int userId, string sortProperty, int pageNumber, int pageSize, out int totalItemCount, out string realSortProperty);
        IEnumerable<Blog> GetList(String filter, string sortProperty, int pageNumber, int pageSize, out int totalItemCount, out string realSortProperty);
    }
}
