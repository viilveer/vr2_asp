using System.ComponentModel.DataAnnotations;
using Domain;

namespace WebAPI.ViewModels.Message
{
    public class CreateModel
    {
        [Required]
        [MaxLength(65535)]
        public string Text { get; set; }

        public Domain.Message GetMessage()
        {
            return new Domain.Message()
            {
                Text = Text,
                Status = MessageStatus.New,
            };
        }
    }

    
}