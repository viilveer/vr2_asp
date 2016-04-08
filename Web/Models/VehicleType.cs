using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class VehicleType
    {
        public int VehicleTypeId { get; set; }
        public string TypeName { get; set; }
        public virtual IList<Vehicle> VehicleTypes { get; set; } = new List<Vehicle>();
    }
}