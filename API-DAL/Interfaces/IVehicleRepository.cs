using System.Collections.Generic;
using Domain;

namespace API_DAL.Interfaces
{
    public interface IVehicleRepository : IApiRepository<Vehicle>
    {
        List<Vehicle> GetListByUserId(int userId, string sortProperty, int pageNumber, int pageSize, out int totalVehicleCount, out string realSortProperty);

        int CountByUserId(int userId);

        Vehicle GetByIdAndUserId(int id, int userId);
    }
}
