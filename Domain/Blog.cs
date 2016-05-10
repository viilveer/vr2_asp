using System;
using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class Blog : BaseEntity
    {
        public int BlogId { get; set; }

        [Display(Name = "Vehicle", ResourceType = typeof(Resources.Domain))]
        [Required]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        [Required]
        [MaxLength(65365)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string HeadLine { get; set; }

    }
}
