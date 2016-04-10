using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class PostComment
    {
        public int PostCommentId { get; set; }

        public virtual BlogPost PostId { get; set; }

        public virtual MultiLangString PostCommentMessage { get; set; }

        public string PostCommentCreatedBy { get; set; }

        public DateTime PostCommentCreatedAt { get; set; }

        public String PostCommentUpdatedBy { get; set; }
        public DateTime PostCommentUpdatedAt { get; set; }
    }
}
