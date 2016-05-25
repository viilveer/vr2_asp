using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain
{
    public class MessageThread : BaseEntity
    {
        [Key]
        public int MessageThreadId { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public virtual UserInt Author { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        public virtual ICollection<MessageThreadReceiver> MessageThreadReceivers { get; set; }
    }
}
