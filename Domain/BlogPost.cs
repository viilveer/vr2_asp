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

        [MaxLength(255)]
        public string  Title { get; set; }

        [Required]
        [MaxLength(65365)]
        public string  Message { get; set; }

    }
}
