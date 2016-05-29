using System.Collections.Generic;
using BLL.ViewModels.Vehicle;
using Domain;
using Interfaces.Repositories;

namespace API_DAL.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        CreateModel AddVehicle(CreateModel model);

        UpdateModel UpdateVehicle(int id, UpdateModel model);

        IEnumerable<IndexVehicleModel> GetUserVehicleList(string sortProperty, int pageNumber, int pageSize, out int totalItemCount,
            out string realSortProperty);

        DetailsModel GetUserVehicle(int id);

    }
}
