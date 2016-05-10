using System;

namespace Web.Areas.MemberArea.ViewModels.Message
{
    public class DetailsModel
    {

        public int Id { get; set; }
        public int AuthorId { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string Text { get; set; }

        public string Sender { get; set; }


        public DateTime StartedDateTime { get; set; }
    }

    public static class DetailsModelFactory
    {
        public static DetailsModel CreateFromMessage(Domain.Message message)
        {
            return new DetailsModel()
            {
                Id = message.MessageId,
                AuthorId = message.AuthorId,
                DateTimeCreated = message.CreatedAt,
                Text = message.Text,
                Sender = message.Author.Email, // TODO :: fix
                StartedDateTime = message.CreatedAt,
            };
        }
    }
}