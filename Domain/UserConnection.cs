using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain
{
    class UserConnection
    {
        public int UserConnectionId { get; set; }

        [Required]
        public User UserId { get; set; }

        [Required]
        public User ConnectionId { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserConnectionCreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UserConnectionCreatedAt { get; set; }

    }
}
