﻿using System;
using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Identity;

namespace Web.Areas.MemberArea.ViewModels.Blog
{
    public class UpdateModel
    {
        public string VehicleName;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string HeadLine { get; set; }

        public Domain.Blog UpdateBlog(Domain.Blog blog, UserInt user)
        {
            blog.UpdatedAt = DateTime.Now;
            blog.UpdatedBy = user.Email;
            blog.Name = new MultiLangString(Name);
            blog.HeadLine = new MultiLangString(HeadLine);
            return blog;
        }
    }
}