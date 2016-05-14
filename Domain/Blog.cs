using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;


namespace Domain
{
    public class Blog : BaseEntity
    {
        public int BlogId { get; set; }

        [Display(Name = "Vehicle", ResourceType = typeof(Resources.Domain))]
        [Required]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        [Required]
        [MaxLength(65365)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string HeadLine { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public virtual UserInt Author { get; set; }
        public virtual ICollection<UserBlogConnection> UserBlogConnections { get; set; }

    }
}
