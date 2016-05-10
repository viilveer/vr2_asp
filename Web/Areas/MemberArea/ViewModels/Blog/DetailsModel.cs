using System.Collections.Generic;
using Web.Areas.MemberArea.ViewModels.BlogPost;

namespace Web.Areas.MemberArea.ViewModels.Blog
{
    public class DetailsModel
    {
        public IEnumerable<BlogPost.DetailsModel> BlogPosts { get; set; }

        public string Name { get; set; }

        public string HeadLine { get; set; }

        public int BlogId { get; set; }
    }
}