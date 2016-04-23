using System;
using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Identity;

namespace Web.Areas.MemberArea.ViewModels.MessageThread
{
    // This model is responsible for holding attributes for creating message thread and message at once
    public class CreateModel
    {
        [Required]
        [MaxLength(512)]
        public string Title { get; set; }

        [Required]
        [MaxLength(65535)]
        public string Text { get; set; }

        public Domain.MessageThread GetMessageThread(UserInt sender, UserInt receiver)
        {
            CheckSenderAndReceiver(sender, receiver);
            return new Domain.MessageThread()
            {
                Title = new MultiLangString(Title),
                ReceiverId = receiver.Id,
                SenderId = sender.Id,
            };
        }


        public Domain.Message GetMessage(UserInt sender, UserInt receiver)
        {
            CheckSenderAndReceiver(sender, receiver);
            return new Domain.Message()
            {
                Text = new MultiLangString(Text),
                ReceiverId = receiver.Id,
                SenderId = sender.Id,
            };
        }

        private void CheckSenderAndReceiver(UserInt sender, UserInt receiver)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (receiver == null) throw new ArgumentNullException(nameof(receiver));
        }
    }
}