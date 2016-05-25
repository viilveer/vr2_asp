using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Helpers
{
    public class PagedList<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalItemCount { get; set; }

        public int PerPage { get; set; }

        public int CurrentPage { get; set; }

        public string RealSortProperty { get; set; }
    }
}