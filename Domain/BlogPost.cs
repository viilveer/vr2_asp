using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class BlogPost
    {
        public int BlogPostId { get; set; }

        public virtual Blog BlogId { get; set; }

        public virtual MultiLangString BlogPostTitle { get; set; }

        public virtual MultiLangString BlogPostMessage { get; set; }

        public string BlogPostCreatedBy { get; set; }

        public DateTime BlogPostCreatedAt { get; set; }

        public String BlogPostUpdatedBy { get; set; }
        public DateTime BlogPostUpdatedAt { get; set; }
    }
}
