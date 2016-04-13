using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class VehicleRepository: EFRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IDbContext dbContext) : base(dbContext)
        {
        }

    }
}
