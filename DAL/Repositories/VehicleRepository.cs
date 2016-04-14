using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class VehicleRepository: EFRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public List<Vehicle> GetAllByUserId(int userId)
        {
            return DbSet.Where(u => u.UserId == userId).ToList();
        }

        public int CountByUserId(int userId)
        {
            return DbSet.Count(a => a.UserId == userId);
        }
    }
}
