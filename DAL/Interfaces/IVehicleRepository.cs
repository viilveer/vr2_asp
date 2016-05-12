using System.Collections.Generic;
using Domain;

namespace DAL.Interfaces
{
    public interface IVehicleRepository : IEFRepository<Vehicle>
    {
        List<Vehicle> GetListByUserId(int userId, string sortProperty, int pageNumber, int pageSize, out int totalVehicleCount, out string realSortProperty);

        int CountByUserId(int userId);
    }
}
