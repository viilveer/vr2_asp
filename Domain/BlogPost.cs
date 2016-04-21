using System;
using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class BlogPost : BaseEntity
    {
        public int BlogPostId { get; set; }

        [Required]
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public virtual MultiLangString Title { get; set; }

        [Required]
        public virtual MultiLangString Message { get; set; }

    }
}
