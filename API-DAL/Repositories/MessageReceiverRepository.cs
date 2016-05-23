using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Interfaces.Repositories;
using Domain;
using Microsoft.Owin.Security;

namespace API_DAL.Repositories
{
    public class MessageReceiverRepository : ApiRepository<MessageReceiver>, IMessageReceiverRepository
    {
        public MessageReceiverRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

    }
}
