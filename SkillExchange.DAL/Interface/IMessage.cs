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
        Task<Message?> GetByIdAsync(int id);
        Task<IEnumerable<Message>> GetMessageBetweenUserAsync(int fromUserId, int toUserId);
        Task<IEnumerable<Message>> GetInboxAsync(int userId);
        Task<IEnumerable<Message>> GetSentMessagesAsync(int userId);

        Task AddAsync(Message message);
        Task DeleteAsync(int id);
        Task MarkAsReadAsync(int messageId);


    }
}
