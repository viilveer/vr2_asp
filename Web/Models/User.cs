using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public int VehicleId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual IList<Blog> Blogs { get; set; } = new List<Blog>();
        public virtual IList<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}