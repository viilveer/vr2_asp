using System.ComponentModel.DataAnnotations;
using Domain;

namespace Web.Areas.MemberArea.ViewModels.Message
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
                Text = new MultiLangString(Text),
                Status = MessageStatus.New,
            };
        }
    }

    
}