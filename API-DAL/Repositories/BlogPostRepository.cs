﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using Domain;
using Interfaces.Repositories;
using Microsoft.Owin.Security;
using NLog;

namespace API_DAL.Repositories
{
    public class BlogPostRepository : ApiRepository<BlogPost>, IBlogPostRepository
    {
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public BlogPostRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {

        }

        public BlogPost GetOneByUserAndId(int id, int userId)
        {
            var response = HttpClient.GetAsync(EndPoint + $"User/Me/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<BlogPost>().Result;
                return res;
            }

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new BlogPost();
        }

        public List<BlogPost> GetAllByBlogId(int blogId, string sortProperty, int pageNumber, int pageSize, out int totalItemCount,
            out string realSortProperty)
        {
            string requestParams =
               $"sortProperty={sortProperty}&pageNumber={pageNumber}&pageSize={pageSize}";

            var response = HttpClient.GetAsync(EndPoint + $"Blog/{blogId}/All?" + requestParams).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<BlogPost>>().Result;
                realSortProperty = response.Headers.GetValues("X-RealSortProperty").ToString();
                totalItemCount = Convert.ToInt32(response.Headers.GetValues("X-Paging-TotalRecordCount").FirstOrDefault());
                return res;
            }
            realSortProperty = "";
            totalItemCount = 0;

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new List<BlogPost>();
        }

        public List<BlogPost> GetDashBoardFavoriteBlogBlogPosts(int userId, int limit)
        {
            var response = HttpClient.GetAsync(EndPoint + "/GetUserBlogPosts/" + limit).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<BlogPost>>().Result;
                return res;
            }

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new List<BlogPost>();
        }

        public List<BlogPost> GetDashBoardNewestBlogPosts(int limit)
        {
            var response = HttpClient.GetAsync(EndPoint + "/GetNewBlogPosts/" + limit).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<BlogPost>>().Result;
                return res;
            }

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new List<BlogPost>();
        }

        public void DeleteByBlogId(int blogId)
        {
            throw new System.NotImplementedException();
        }
    }
}
