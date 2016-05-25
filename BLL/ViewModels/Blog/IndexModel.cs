using PagedList;

namespace BLL.ViewModels.Blog
{
    public class IndexModel
    {
        public IPagedList<Domain.Blog> Blogs { get; set; }

        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 25;
        public string SortProperty { get; set; }
        public string Filter { get; set; }
    }
}