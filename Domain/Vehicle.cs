using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class Vehicle : BaseEntity
    {
        public int VehicleId { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual UserInt User { get; set; }


        [Required]
        [MaxLength(64)]
        [Display(Name = "Make", ResourceType = typeof(Resources.Vehicles))]
        public string Make { get; set; }

        [Required]
        [MaxLength(64)]
        [Display(Name = "Model", ResourceType = typeof(Resources.Vehicles))]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Year", ResourceType = typeof(Resources.Vehicles))]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Kw", ResourceType = typeof(Resources.Vehicles))]
        public int Kw { get; set; }

        [MaxLength(64)]
        [Required]
        [Display(Name = "Engine", ResourceType = typeof(Resources.Vehicles))]
        public string Engine { get; set; }

        public Blog Blog { get; set; }
    }
}
