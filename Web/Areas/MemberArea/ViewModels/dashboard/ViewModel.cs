using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.MemberArea.ViewModels.Dashboard
{
    public class ViewModel
    {
        public int VehicleCount { get; set; }

        public List<Domain.BlogPost> NewBlogPostList { get; set; } = new List<Domain.BlogPost>();
        public List<Domain.BlogPost> FavoriteBlogPostList { get; set; } = new List<Domain.BlogPost>();

    }
}