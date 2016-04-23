using System.Collections.Generic;
using Domain;

namespace DAL.Interfaces
{
    public interface IVehicleRepository : IEFRepository<Vehicle>
    {
        List<Vehicle> GetAllByUserId(int userId);

        int CountByUserId(int userId);
    }
}
