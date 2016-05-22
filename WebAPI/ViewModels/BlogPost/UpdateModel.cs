using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Domain;
using Domain.Identity;

namespace WebAPI.ViewModels.BlogPost
{
    public class UpdateModel
    {
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(65535)]
        [MinLength(32)]
        [AllowHtml]
        public string Message { get; set; }


        public Domain.BlogPost UpdateBlogPost(Domain.BlogPost blogPost)
        {
            blogPost.Message = Message;
            blogPost.Title = Title;
            return blogPost;
        }
    }

    public static class UpdateModelFactory
    {
        public static UpdateModel CreateFromBlogPost(Domain.BlogPost blogPost)
        {
            return new UpdateModel()
            {
                Title = blogPost.Title,
                Message = blogPost.Message,
            };
        }
    }
}