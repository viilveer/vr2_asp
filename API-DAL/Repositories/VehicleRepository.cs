using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Interfaces.Repositories;
using Domain;
using Microsoft.Owin.Security;

namespace API_DAL.Repositories
{
    public class VehicleRepository: ApiRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
        public List<Vehicle> GetListByUserId(int userId, string sortProperty, int pageNumber, int pageSize, out int totalVehicleCount,
            out string realSortProperty)
        {
            throw new System.NotImplementedException();
        }

        public int CountByUserId(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Vehicle GetByIdAndUserId(int id, int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
