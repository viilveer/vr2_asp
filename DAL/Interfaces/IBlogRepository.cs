using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Domain;

namespace DAL.Interfaces
{
    public interface IBlogRepository : IEFRepository<Blog>
    {
        IEnumerable<Blog> GetListByUserId(int getUserId, string sortProperty, int i, int value, out int totalBlogCount, out string realSortProperty);
    }
}
