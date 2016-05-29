using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net.Http;
using API_DAL.Interfaces;
using BLL.ViewModels.Vehicle;
using Domain;
using Microsoft.Owin.Security;
using NLog;

namespace API_DAL.Repositories
{
    public class VehicleRepository: ApiRepository<Vehicle>, IVehicleRepository
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public VehicleRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }


        public CreateModel AddVehicle(CreateModel model)
        {
            var response = HttpClient.PostAsJsonAsync(EndPoint, model).Result;
            if (!response.IsSuccessStatusCode)
            {

                _logger.Error(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
            return response.Content.ReadAsAsync<CreateModel>().Result;
        }

        public UpdateModel UpdateVehicle(int id, UpdateModel model)
        {
            var response = HttpClient.PutAsJsonAsync(EndPoint + $"{id}", model).Result;
            if (!response.IsSuccessStatusCode)
            {

                _logger.Error(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
                throw new Exception(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            }
            return response.Content.ReadAsAsync<UpdateModel>().Result;
        }


        public IEnumerable<IndexVehicleModel> GetUserVehicleList(string sortProperty, int pageNumber, int pageSize, out int totalItemCount, out string realSortProperty)
        {
            string requestParams =
                $"sortProperty={sortProperty}&pageNumber={pageNumber}&pageSize={pageSize}";

            var response = HttpClient.GetAsync(EndPoint + "?" + requestParams).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<List<IndexVehicleModel>>().Result;
                realSortProperty = response.Headers.GetValues("X-RealSortProperty").ToString();
                totalItemCount = Convert.ToInt32(response.Headers.GetValues("X-Paging-TotalRecordCount").FirstOrDefault());
                return res;
            }
            realSortProperty = "";
            totalItemCount = 0;

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            return new List<IndexVehicleModel>();
        }
      
        public DetailsModel GetUserVehicle(int id)
        {
            var response = HttpClient.GetAsync(EndPoint + $"{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<DetailsModel>().Result;
                return res;
            }

            _logger.Debug(response.RequestMessage.RequestUri + " - " + response.StatusCode + " - " + response.ReasonPhrase);
            throw new ObjectNotFoundException("Vehicle not found");
        }
    }
}
