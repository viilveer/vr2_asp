﻿using System;
using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }

        [Required]
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public virtual MultiLangString Title { get; set; }

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
