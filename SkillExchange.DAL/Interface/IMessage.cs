using SkillExchange.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Interface
{
    public interface IMessage
    {
        Message?GetById(int id);
        IEnumerable<Message>GetMessageBetweenUser(int fromUserId, int toUserId);
        IEnumerable<Message> GetInbox(int userId);
        IEnumerable<Message>GetSentMessages(int userId);
        void Add(Message message);
        void Delete(int id);
        void MarkAsRead(int messageId);


    }
}
