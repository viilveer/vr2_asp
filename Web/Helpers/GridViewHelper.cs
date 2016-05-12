using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Helpers
{
    public class GridViewHelper
    {
        public static string GetSortProperty(string propertyName , String currentSortProperty )
        {
            return currentSortProperty == propertyName ? "_" + propertyName : propertyName;
        }

        public static string GetSortIcon(string propertyName , String currentSortProperty )
        {
            return currentSortProperty == propertyName ? "fa-sort-up" : currentSortProperty == "_" + propertyName ? "fa-sort-down" : "fa-sort";
        } 
    }
}