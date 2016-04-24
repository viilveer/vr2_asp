
using Domain;

namespace Web.Helpers
{
    public static class MessagesHelper
    {
        public static int ResolveMessageReceiver(int activeUserId, MessageThread thread)
        {
            return thread.ReceiverId == activeUserId ? thread.SenderId : thread.ReceiverId;
        }

        public static int ResolveMessageSender(int activeUserId, MessageThread thread)
        {
            return thread.ReceiverId == activeUserId ? thread.ReceiverId : thread.SenderId;
        }

        public static Message AssignMessageReceiverAndSender(Message message, int activeUserId, MessageThread thread)
        {
            message.ReceiverId = ResolveMessageReceiver(activeUserId, thread);
            message.SenderId = ResolveMessageSender(activeUserId, thread);
            return message;
        }
    }
}