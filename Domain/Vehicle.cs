using System;
using Domain.Identity;

namespace Domain
{
    class Vehicle
    {
        public int VehcileId { get; set; }

        public User UserId { get; set; }

        public string VehicleMake { get; set; }

        public string VehicleModel { get; set; }

        public int VehicleYear { get; set; }

        public int VehicleKw { get; set; }

        public string VehicleEngine { get; set; }

        public string VehicleCreatedBy { get; set; }

        public DateTime VehicleCreatedAt { get; set; }

        public String VehicleUpdatedBy { get; set; }
        public DateTime VehicleUpdatedAt { get; set; }


    }
}
