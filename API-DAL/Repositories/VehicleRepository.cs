using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BLL.ViewModels.Vehicle;
using Interfaces.Repositories;
using Domain;
using Microsoft.Owin.Security;
using NLog;

namespace API_DAL.Repositories
{
    public class VehicleRepository: ApiRepository<Vehicle>, IVehicleRepository
    {
        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        public VehicleRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
        public List<Vehicle> GetListByUserId(int userId, string sortProperty, int pageNumber, int pageSize, out int totalItemCount,
            out string realSortProperty)
        {
            string requestParams =
                $"sortProperty={sortProperty}&pageNumber={pageNumber}&pageSize={pageSize}";

            var response = HttpClient.GetAsync(EndPoint + "?" + requestParams).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<Vehicle>>().Result;
                realSortProperty = response.Headers.GetValues("X-RealSortProperty").ToString();
                totalItemCount = Convert.ToInt32(response.Headers.GetValues("X-Paging-TotalRecordCount").FirstOrDefault());
                return res;
            }
            realSortProperty = "";
            totalItemCount = 0;

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new List<Vehicle>();
        }
       
        public int CountByUserId(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Vehicle GetByIdAndUserId(int id, int userId)
        {
            var response = HttpClient.GetAsync(EndPoint + $"User/Me/Vehicle/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<Vehicle>().Result;
                return res;
            }

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new Vehicle();
        }
    }
}
