using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class MessageReceiver : BaseEntity
    {
        [Key]
        public int MessageReceiverId { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual UserInt User { get; set; }

        [Required]
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }


    }
}
