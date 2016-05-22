//using PagedList;

namespace WebAPI.ViewModels.Blog
{
    public class DetailsModel
    {
        //public IPagedList<Domain.BlogPost> BlogPosts { get; set; }

        public string Name { get; set; }

        public string HeadLine { get; set; }

        public int BlogId { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortProperty { get; set; }
    }
}