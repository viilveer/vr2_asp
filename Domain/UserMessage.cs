using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain
{
    class UserMessage
    {

        public int UserMessageId { get; set; }

        public User UserMessageUserId { get; set; }
        public User ReceiverId { get; set; }

        public virtual MultiLangString Message { get; set; }

        public string UserMessageCreatedBy { get; set; }

        public DateTime UserMessageCreatedAt { get; set; }

        public String UserMessageUpdatedBy { get; set; }
        public DateTime UserMessageUpdatedAt { get; set; }
    }
}
