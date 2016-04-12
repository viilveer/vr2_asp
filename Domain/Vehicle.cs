using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual UserInt User { get; set; }


        [Required]
        [MaxLength(64)]
        public string Make { get; set; }

        [Required]
        [MaxLength(64)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Kw { get; set; }

        [MaxLength(64)]
        [Required]
        public string Engine { get; set; }

        [MaxLength(255)]
        [Required]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedAt { get; set; }

        [MaxLength(255)]
        public String UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }


    }
}
