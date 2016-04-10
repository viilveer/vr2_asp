using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class PostComment
    {
        public int PostCommentId { get; set; }

        [Required]
        public virtual BlogPost PostId { get; set; }

        [Required]
        public virtual MultiLangString PostCommentMessage { get; set; }

        [Required]
        [MaxLength(255)]
        public string PostCommentCreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostCommentCreatedAt { get; set; }

        [MaxLength(255)]
        public String PostCommentUpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PostCommentUpdatedAt { get; set; }
    }
}
