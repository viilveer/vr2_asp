using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Interfaces.Repositories;
using Domain;
using Microsoft.Owin.Security;
using NLog;

namespace API_DAL.Repositories
{
    public class BlogRepository : ApiRepository<Blog>, IBlogRepository
    {

        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
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
            string requestParams =
                $"blogName={filter}&sortProperty={sortProperty}&pageNumber={pageNumber}&pageSize={pageSize}";

            var response = HttpClient.GetAsync(EndPoint + "?" + requestParams).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<IEnumerable<Blog>>().Result;
                _logger.Debug(response.Headers.ToString);
                realSortProperty = response.Headers.GetValues("X-RealSortProperty").ToString();
                totalItemCount = Convert.ToInt32(response.Headers.GetValues("X-Paging-TotalRecordCount"));
                return res;
            }
            realSortProperty = "test";
            totalItemCount = 2;

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new List<Blog>();
        }
    }
}
