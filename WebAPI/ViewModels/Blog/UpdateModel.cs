using System;
using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Identity;

namespace WebAPI.ViewModels.Blog
{
    public class UpdateModel
    {
        public string VehicleName;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string HeadLine { get; set; }

        public Domain.Blog UpdateBlog(Domain.Blog blog)
        {
            blog.Name = Name;
            blog.HeadLine = HeadLine;
            return blog;
        }
    }
}
