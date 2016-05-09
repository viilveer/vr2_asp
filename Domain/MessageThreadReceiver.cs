using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain
{
    public class MessageThreadReceiver : BaseEntity
    {
        public int MessageThreadReceiverId { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual UserInt User { get; set; }

        [Required]
        public int MessageThreadId { get; set; }
        public virtual MessageThread MessageThread { get; set; }

    }
}
