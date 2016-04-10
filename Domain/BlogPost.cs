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

        [Required]
        public virtual Blog BlogId { get; set; }

        public virtual MultiLangString BlogPostTitle { get; set; }

        [Required]
        public virtual MultiLangString BlogPostMessage { get; set; }

        [Required]
        [MaxLength(255)]
        public string BlogPostCreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BlogPostCreatedAt { get; set; }

        [MaxLength(255)]
        public String BlogPostUpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BlogPostUpdatedAt { get; set; }
    }
}
