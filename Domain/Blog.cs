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
        public Vehicle VehicleId { get; set; }

        public virtual MultiLangString BlogName { get; set; }

        public virtual MultiLangString BlogHeadLine { get; set; }

        public string BlogCreatedBy { get; set; }

        public DateTime BlogCreatedAt { get; set; }

        public String BlogUpdatedBy { get; set; }
        public DateTime BlogUpdatedAt { get; set; }
    }
}
