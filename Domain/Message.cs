using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class Message : BaseEntity
    {

        public int MessageId { get; set; }

        [Required]
        public int MessageThreadId { get; set; }
        public virtual MessageThread MessageThread { get; set; }

        [Required]
        public int SenderId { get; set; }
        public virtual UserInt Sender { get; set; }

        [Required]
        public int ReceiverId { get; set; }
        public virtual UserInt Receiver { get; set; }

        [Required]
        public virtual MultiLangString Text { get; set; }

        [Required]
        public MessageStatus Status { get; set; }
    }

    public enum MessageStatus
    {
        New,
        Read,
        Replied
    }
}
