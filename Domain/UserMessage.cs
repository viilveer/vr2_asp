using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain
{
    class UserMessage
    {

        public int UserMessageId { get; set; }

        [Required]
        public User UserMessageUserId { get; set; }

        [Required]
        public User ReceiverId { get; set; }

        public virtual MultiLangString UserMessageTitle { get; set; }

        [Required]
        public virtual MultiLangString Message { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserMessageCreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UserMessageCreatedAt { get; set; }

        [MaxLength(255)]
        public String UserMessageUpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UserMessageUpdatedAt { get; set; }
    }
}
