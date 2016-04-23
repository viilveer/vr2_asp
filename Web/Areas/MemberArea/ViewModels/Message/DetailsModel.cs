using System;

namespace Web.Areas.MemberArea.ViewModels.Message
{
    public class DetailsModel
    {

        public string Text { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public DateTime StartedDateTime { get; set; }
    }

    public static class DetailsModelFactory
    {
        public static DetailsModel CreateFromMessage(Domain.Message message)
        {
            return new DetailsModel()
            {
                Text = message.Text.Value,
                Sender = message.Sender.Email, // TODO :: fix
                Receiver = message.Receiver.Email, // TODO :: fix
                StartedDateTime = message.CreatedAt,
            };
        }
    }
}