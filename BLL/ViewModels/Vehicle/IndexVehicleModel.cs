using System;

namespace BLL.ViewModels.Vehicle
{
    public class IndexVehicleModel
    {
       
        public int Id { get; set; }
        public string Make { get; set; }

        public string Model { get; set; }

    
        public int Year { get; set; }

        public int Kw { get; set; }

        public string Engine { get; set; }

        public DateTime CreateDateTime{ get; set; }
    }

    public static class IndexVehicleModelFactory
    {
        public static IndexVehicleModel CreateFromVehicle(Domain.Vehicle vehicle)
        {
            return new IndexVehicleModel() {
                Id = vehicle.VehicleId,
                Kw = vehicle.Kw,
                Engine = vehicle.Engine,
                Year = vehicle.Year,
                Model = vehicle.Model,
                Make = vehicle.Make,
                CreateDateTime = vehicle.CreatedAt
            };
        }
    }
}
