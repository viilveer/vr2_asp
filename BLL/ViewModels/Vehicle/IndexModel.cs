using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace BLL.ViewModels.Vehicle
{
    public class IndexModel
    {
        public IPagedList<IndexVehicleModel> Vehicles { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortProperty { get; set; }
    }
}