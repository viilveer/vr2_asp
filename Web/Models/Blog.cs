using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
        public string BlogContent { get; set; }
        public string BlogTopic { get; set; }
        public User User { get; set; }
        public virtual IList<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

    }
}