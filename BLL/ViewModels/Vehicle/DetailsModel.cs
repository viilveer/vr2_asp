using System;

namespace BLL.ViewModels.Vehicle
{
    public class DetailsModel
    {
       
        public int Id { get; set; }
        public string Make { get; set; }

        public string Model { get; set; }

    
        public int Year { get; set; }

        public int Kw { get; set; }

        public string Engine { get; set; }

        public string Author { get; set; }
    }


    public static class DetailsModelFactory
    {
        public static DetailsModel CreateFromVehicle(Domain.Vehicle vehicle)
        {
            return new DetailsModel()
            {
                Id = vehicle.VehicleId,
                Kw = vehicle.Kw,
                Engine = vehicle.Engine,
                Year = vehicle.Year,
                Model = vehicle.Model,
                Make = vehicle.Make,
                Author = vehicle.User.FirstLastName
            };
        }
    }
}
