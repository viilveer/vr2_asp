using System.Collections.Generic;
using System.Linq;
using API_DAL.Interfaces;
using Domain;

namespace API_DAL.Repositories
{
    public class VehicleRepository: ApiRepository<Vehicle>, IVehicleRepository
    {
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
