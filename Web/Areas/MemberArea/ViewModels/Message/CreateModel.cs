using System.ComponentModel.DataAnnotations;

namespace Web.Areas.MemberArea.ViewModels.Message
{
    public class CreateModel
    {
        [Required]
        [MaxLength(65535)]
        public string Text { get; set; }
    }
}