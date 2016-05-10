
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Domain;
using Domain.Identity;

namespace Web.Areas.MemberArea.ViewModels.BlogPost
{
    public class CreateModel
    {
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(65535)]
        [MinLength(32)]
        [AllowHtml]
        public string Message { get; set; }

        public Domain.BlogPost GetBlogPost(Domain.Blog blog)
        {
            return new Domain.BlogPost()
            {
                BlogId = blog.BlogId,
                Title = Title,
                Message = Message
            };
        }
    }
}