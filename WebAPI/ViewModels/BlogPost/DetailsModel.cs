
using System;

namespace WebAPI.ViewModels.BlogPost
{
    public class DetailsModel
    {
        public int BlogPostId { get; set; }

        public string Title { get; set; }
        public string AuthorName { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public static class DetailsModelFactory
    {
        public static DetailsModel CreateFromBlogPost(Domain.BlogPost blogPost)
        {
            return new DetailsModel()
            {
                BlogPostId = blogPost.BlogPostId,
                Title = blogPost.Title,
                AuthorName = blogPost.Author.FirstLastName,
                Message = blogPost.Message,
                CreatedBy = blogPost.CreatedBy,
                CreatedAt = blogPost.CreatedAt,
                UpdatedAt = blogPost.UpdatedAt
            };
        }
    }
}