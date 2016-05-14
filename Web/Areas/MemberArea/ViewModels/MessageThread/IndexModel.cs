using PagedList;

namespace Web.Areas.MemberArea.ViewModels.MessageThread
{
    public class IndexModel
    {
        public IPagedList<Domain.MessageThread> Messages { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortProperty { get; set; }
    }
}