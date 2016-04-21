using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Domain;
using Domain.Identity;

namespace Web.Areas.MemberArea.ViewModels.BlogPost
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


        public Domain.BlogPost UpdateBlogPost(Domain.BlogPost blogPost, UserInt updater)
        {
            blogPost.UpdatedAt = DateTime.Now;
            blogPost.Message = new MultiLangString(Message);
            blogPost.Title = new MultiLangString(Title);
            blogPost.UpdatedBy = updater.Email;
            return blogPost;
        }
    }

    public class UpdateModelFactory
    {
        public static UpdateModel CreateFromBlogPost(Domain.BlogPost blogPost)
        {
            return new UpdateModel()
            {
                Title = blogPost.Title.Value,
                Message = blogPost.Message.Value,
            };
        }
    }
}