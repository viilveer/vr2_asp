using PagedList;

namespace Web.Areas.MemberArea.ViewModels.Blog
{
    public class IndexModel
    {
        public IPagedList<Domain.Blog> Blogs { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortProperty { get; set; }
        public string Filter { get; set; }
    }
}