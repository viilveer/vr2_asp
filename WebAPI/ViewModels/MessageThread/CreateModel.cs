using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain;
using Domain.Identity;

namespace WebAPI.ViewModels.MessageThread
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

        public NewThreadModel GetNewThreadModel(UserInt sender, UserInt receiver)
        {
            NewThreadModel model = new NewThreadModel(sender, receiver);
            model.Prepare(Title, Text);
            return model;
        }
    }

    public class NewThreadModel
    {
        private readonly UserInt _sender;
        private readonly UserInt _receiver;
        public Domain.MessageThread MessageThread { get; private set; }
        public Domain.Message Message { get; private set; }
        public List<MessageThreadReceiver> MessageThreadReceivers{ get; private set; }
        public List<MessageReceiver> MessageReceivers { get; private set; }


        public NewThreadModel(UserInt sender, UserInt receiver)
        {
            _sender = sender;
            _receiver = receiver;
            CheckSenderAndReceiver(sender, receiver);
        }

        public void Prepare(string title, string text)
        {
            PrepareMessageThread(title);
            PrepareMessageThreadReceivers();
            PrepareMessage(text);
            PrepareMessageReceivers();
        }

        private void PrepareMessageReceivers()
        {
            MessageReceivers = new List<int>() {_sender.Id, _receiver.Id}.Select(id => new MessageReceiver() {UserId = id}).ToList();
        }

        private void PrepareMessageThreadReceivers()
        {
            MessageThreadReceivers = new List<int>() {_sender.Id, _receiver.Id}.Select(id => new MessageThreadReceiver() {UserId = id}).ToList();
        }

        private void PrepareMessageThread(string title)
        {
           
            MessageThread = new Domain.MessageThread()
            {
                Title = title,
                AuthorId = _sender.Id,
            };
        }


        private void PrepareMessage(string text)
        {
            Message = new Domain.Message()
            {
                Text = text,
                AuthorId = _sender.Id,
                Status = MessageStatus.New,
            };
        }


        private void CheckSenderAndReceiver(UserInt sender, UserInt receiver)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (receiver == null) throw new ArgumentNullException(nameof(receiver));
        }

    }
}