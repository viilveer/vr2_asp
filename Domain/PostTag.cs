using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PostTag : BaseEntity
    {

        public int PostTagId { get; set; }

        [Required]
        public int BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }

        [Required]
        public virtual MultiLangString Value { get; set; }
    }
}
