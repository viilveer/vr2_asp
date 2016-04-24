using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain
{
    // TODO :: add MessageThreadUser table for users to add delete feature to users, remove sender and receiver, messagehelper must be altered after this refactor
    public class MessageThread : BaseEntity
    {

        public int MessageThreadId { get; set; }

        [Required]
        public int SenderId { get; set; }
        public virtual UserInt Sender { get; set; }

        [Required]
        public int ReceiverId { get; set; }
        public virtual UserInt Receiver { get; set; }

        [Required]
        public virtual MultiLangString Title { get; set; }
    }
}
