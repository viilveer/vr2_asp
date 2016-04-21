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
        public virtual MultiLangString Name { get; set; }

        public virtual MultiLangString HeadLine { get; set; }

    }
}
