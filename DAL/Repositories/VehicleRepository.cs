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

        public List<Vehicle> GetListByUserId(int userId, string sortProperty, int pageNumber, int pageSize, out int totalVehicleCount, out string realSortProperty)
        {
            sortProperty = sortProperty?.ToLower() ?? "";
            realSortProperty = sortProperty;


            //start building up the query
            var res = DbSet
                .Where(p => p.UserId == userId);


            // set up sorting
            switch (sortProperty)
            {
                case "_model":
                    res = res
                        .OrderBy(o => o.Model).ThenBy(o => o.Make);
                    break;

                default:
                case "_make":
                    res = res
                        .OrderBy(o => o.Make).ThenBy(o => o.Model);
                    realSortProperty = "_make";
                    break;
            }

            totalVehicleCount = res.Count();

            var reslist = res
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return reslist;
        }

        public int CountByUserId(int userId)
        {
            return DbSet.Count(a => a.UserId == userId);
        }

        public Vehicle GetByIdAndUserId(int id, int userId)
        {
            return DbSet.FirstOrDefault(x => x.UserId == userId && x.VehicleId == id);
        }
    }
}
