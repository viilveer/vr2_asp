using System.Collections.Generic;

namespace Web.Areas.MemberArea.ViewModels.MessageThread
{
    public class DetailsModel
    {
        public List<Message.DetailsModel> DetailsModels { get; set; }

        public ViewModels.Message.CreateModel NewMessageModel { get; set; }

        public string Title { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }
    }
}