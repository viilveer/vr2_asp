using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain
{
    class UserConnection
    {
        public int UserConnectionId { get; set; }

        public User UserId { get; set; }

        public User ConnectionId { get; set; }

        public string UserConnectionCreatedBy { get; set; }

        public DateTime UserConnectionCreatedAt { get; set; }

    }
}
