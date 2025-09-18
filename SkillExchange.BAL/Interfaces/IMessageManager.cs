using SkillExchange.API.DTO.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.BAL.Interfaces
{
    public interface IMessageManager
    {
        Task<MessageDTO> SendMessageAsync(int fromUserId, SendMessageDTO dto);
        Task<IEnumerable<MessageDTO>> GetInboxAsync(int userId);
        Task<IEnumerable<MessageDTO>> GetSentAsync(int userId);
        Task<IEnumerable<MessageDTO>> GetConversationAsync(int userAId, int userBId);
        Task MarkAsReadAsync(int userId, int messageId); 
        Task DeleteAsync(int userId, int messageId);
    }
}
