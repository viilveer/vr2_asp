using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PostComment : BaseEntity
    {
        public int PostCommentId { get; set; }

        [Required]
        public int BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
       
        [Required]
        public virtual MultiLangString Message { get; set; }
    }
}
