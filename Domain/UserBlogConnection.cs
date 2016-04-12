using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class UserBlogConnection
    {
        public int UserBlogConnectionId { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual UserInt User { get; set; }

        [Required]
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        [Required]
        [MaxLength(255)]
        public string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

    }
}
