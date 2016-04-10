using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class Blog
    {
        public int BlogId { get; set; }

        [Display(Name = "Vehicle", ResourceType = typeof(Resources.Domain))]
        [Required]
        public Vehicle VehicleId { get; set; }

        [Required]
        public virtual MultiLangString BlogName { get; set; }

        public virtual MultiLangString BlogHeadLine { get; set; }

        [Required]
        [MaxLength(255)]
        public string BlogCreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BlogCreatedAt { get; set; }

        [MaxLength(255)]
        public String BlogUpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BlogUpdatedAt { get; set; }
    }
}
