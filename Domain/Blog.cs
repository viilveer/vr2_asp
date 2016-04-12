using System;
using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class Blog
    {
        public int BlogId { get; set; }

        [Display(Name = "Vehicle", ResourceType = typeof(Resources.Domain))]
        [Required]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        [Required]
        public virtual MultiLangString Name { get; set; }

        public virtual MultiLangString HeadLine { get; set; }

        [Required]
        [MaxLength(255)]
        public string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [MaxLength(255)]
        public String UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
