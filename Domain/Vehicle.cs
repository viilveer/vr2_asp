using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    class Vehicle
    {
        public int VehcileId { get; set; }

        public User UserId { get; set; }

        [Required]
        [MaxLength(64)]
        public string VehicleMake { get; set; }

        [Required]
        [MaxLength(64)]
        public string VehicleModel { get; set; }

        [MaxLength(4)]
        [MinLength(4)]
        [Required]
        public int VehicleYear { get; set; }

        [MaxLength(4)]
        [Required]
        public int VehicleKw { get; set; }

        [MaxLength(64)]
        [Required]
        public string VehicleEngine { get; set; }

        [MaxLength(255)]
        [Required]
        public string VehicleCreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime VehicleCreatedAt { get; set; }

        [MaxLength(255)]
        public String VehicleUpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime VehicleUpdatedAt { get; set; }


    }
}
