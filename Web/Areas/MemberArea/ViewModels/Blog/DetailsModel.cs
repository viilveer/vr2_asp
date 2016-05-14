using System.Collections.Generic;
using PagedList;
using Web.Areas.MemberArea.ViewModels.BlogPost;

namespace Web.Areas.MemberArea.ViewModels.Blog
{
    public class DetailsModel
    {
        public IPagedList<BlogPost.DetailsModel> BlogPosts { get; set; }

        public string Name { get; set; }

        public string HeadLine { get; set; }

        public int BlogId { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortProperty { get; set; }
    }
}