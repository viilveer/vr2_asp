﻿using System.Collections.Generic;
using Domain;

namespace DAL.Interfaces
{
    public interface IMessageThreadRepository : IEFRepository<MessageThread>
    {
        List<MessageThread> GetAllUserThreads(int userId);
        MessageThread GetUserThread(int threadId, int userId);
        List<MessageThread> GetAllBySenderId(int userId);
        List<MessageThread> GetAllByReceiverId(int userId);
    }
}
