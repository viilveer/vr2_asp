using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class Message : BaseEntity
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public int MessageThreadId { get; set; }
        public virtual MessageThread MessageThread { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public virtual UserInt Author { get; set; }

        [Required]
        [MaxLength(65365)]
        public string Text { get; set; }

        [Required]
        public MessageStatus Status { get; set; }

        public ICollection<MessageReceiver> MessageReceivers { get; set; }
    }

    public enum MessageStatus
    {
        New,
        Read,
        Replied
    }
}
