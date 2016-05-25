using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PostComment : BaseEntity
    {
        [Key]
        public int PostCommentId { get; set; }

        [Required]
        public int BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
       
        [Required]
        [MaxLength(9000)]
        public string Message { get; set; }
    }
}
