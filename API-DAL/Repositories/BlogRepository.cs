using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Interfaces.Repositories;
using Domain;
using Microsoft.Owin.Security;

namespace API_DAL.Repositories
{
    public class BlogRepository : ApiRepository<Blog>, IBlogRepository
    {
        public BlogRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
        public Blog GetOneByUserAndId(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Blog> GetListByUserId(int userId, string sortProperty, int pageNumber, int pageSize, out int totalItemCount,
            out string realSortProperty)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Blog> GetList(string filter, string sortProperty, int pageNumber, int pageSize, out int totalItemCount,
            out string realSortProperty)
        {
            throw new NotImplementedException();
        }
    }
}
