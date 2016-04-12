using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PostComment
    {
        public int PostCommentId { get; set; }

        [Required]
        public int BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
       
        [Required]
        public virtual MultiLangString Message { get; set; }

        [Required]
        [MaxLength(255)]
        public string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [MaxLength(255)]
        public String UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
