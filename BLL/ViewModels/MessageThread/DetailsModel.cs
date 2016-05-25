using System.Collections.Generic;

namespace BLL.ViewModels.MessageThread
{
    public class DetailsModel
    {
        public List<Message.DetailsModel> DetailsModels { get; set; }

        public ViewModels.Message.CreateModel NewMessageModel { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Receiver { get; set; }
    }
}