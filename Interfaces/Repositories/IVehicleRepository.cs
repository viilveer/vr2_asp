using System.Collections.Generic;
using Domain;

namespace Interfaces.Repositories
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        List<Vehicle> GetListByUserId(int userId, string sortProperty, int pageNumber, int pageSize, out int totalVehicleCount, out string realSortProperty);

        int CountByUserId(int userId);

        Vehicle GetByIdAndUserId(int id, int userId);
    }
}
