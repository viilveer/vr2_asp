using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Web.Areas.MemberArea.ViewModels.Vehicle
{
    public class IndexModel
    {
        public IPagedList<Domain.Vehicle> Vehicles { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortProperty { get; set; }
        public string Filter { get; set; }
    }
}