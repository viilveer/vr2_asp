using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string BlogPostContent { get; set; }
        public virtual IList<BlogMedia> BlogMedia { get; set; } = new List<BlogMedia>();
    }
}