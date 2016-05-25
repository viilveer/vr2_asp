using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class UserBlogConnection : BaseEntity
    {
        [Key]
        public int UserBlogConnectionId { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual UserInt User { get; set; }

        [Required]
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

    }
}
