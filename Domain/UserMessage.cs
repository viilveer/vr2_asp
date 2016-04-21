using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class UserMessage : BaseEntity
    {

        public int UserMessageId { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual UserInt User { get; set; }

        [Required]
        public int ReceiverId { get; set; }
        public virtual UserInt Receiver { get; set; }

        [Required]
        public virtual MultiLangString Message { get; set; }
    }
}
