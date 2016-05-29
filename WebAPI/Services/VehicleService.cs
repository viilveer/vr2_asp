using System.Collections.Generic;
using System.Linq;
using BLL.ViewModels.Vehicle;
using Domain;
using Interfaces.Repositories;

namespace WebAPI.Services
{
    class VehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public List<IndexVehicleModel> GetUserVehiclesList(int userId, string sortProperty, int pageNumber, int pageSize, out int totalVehicleCount, out string realSortProperty)
        {
            List<Vehicle> vehicles =_vehicleRepository.GetListByUserId(userId, sortProperty, pageNumber, pageSize, out totalVehicleCount, out realSortProperty);
            return vehicles.Select(IndexVehicleModelFactory.CreateFromVehicle).ToList();
        }

        public DetailsModel GetUserVehicle(int userId, int vehicleId)
        {
            DetailsModel model = null;
            Vehicle vehicle = _vehicleRepository.GetByIdAndUserId(vehicleId, userId);
            if (vehicle != null)
            {
                model = DetailsModelFactory.CreateFromVehicle(vehicle);
            }
            return model;
        }
    }
}
