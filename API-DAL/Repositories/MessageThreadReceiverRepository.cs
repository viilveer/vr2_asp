using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Interfaces.Repositories;
using Domain;
using Microsoft.Owin.Security;

namespace API_DAL.Repositories
{
    public class MessageThreadReceiverRepository : ApiRepository<MessageThreadReceiver>, IMessageThreadReceiverRepository
    {
        public MessageThreadReceiverRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

    }
}
