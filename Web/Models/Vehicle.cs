using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Vehicle
    {

        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public string VehicleName { get; set; }
        public virtual IList<VehicleType> VehicleTypes { get; set; } = new List<VehicleType>();
        public VehicleType VehicleType { get; set; }
        public User User { get; set; }
        
    }
}