using Microsoft.Extensions.Logging;
using SkillExchange.API.DTO.Message;
using SkillExchange.BAL.Interfaces;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkillExchange.DAL.Enums.Enum;

namespace SkillExchange.BAL.Manager
{
    public class MessageManager : IMessageManager
    {
        private readonly IMessage _messageRepo;
        private readonly IAppUser _userRepo;
        private readonly ILogger<MessageManager> _logger;

        public MessageManager(IMessage messageRepo, IAppUser userRepo, ILogger<MessageManager> logger)
        {
            _messageRepo = messageRepo;
            _userRepo = userRepo;
            _logger = logger;
        }
        public async Task DeleteAsync(int userId, int messageId)
        {
            var message = await _messageRepo.GetByIdAsync(messageId);
            if (message == null) throw new KeyNotFoundException("Message not found.");
            var allowed = message.FromUserId == userId || message.ToUserId == userId;
            if (!allowed)
            {
                var isAdmin = await _userRepo.UserHasRoleAsync(userId, UserRoleType.Admin);
                if (!isAdmin) throw new UnauthorizedAccessException("Not allowed to delete this message.");
            }
            await _messageRepo.DeleteAsync(messageId);
        }
        private MessageDTO ToDto(Message m)
        {
            return new MessageDTO
            {
                Id = m.Id,
                FromUserId = m.FromUserId,
                FromUserName_FullName = m.FromUser?.FullName ?? "Unknown",
                ToUserId = m.ToUserId,
                ToUserName_FullName = m.ToUser?.FullName ?? "Unknown",
                Content = m.Content,
                IsRead = m.IsRead,
                SentAt = m.SentAt
            };
        }


        public async Task<IEnumerable<MessageDTO>> GetConversationAsync(int userAId, int userBId)
        {
            var messages = await _messageRepo.GetMessageBetweenUserAsync(userAId, userBId);
            return messages.Select(m => ToDto(m));
        }

        public async Task<IEnumerable<MessageDTO>> GetInboxAsync(int userId)
        {
            var messages = await _messageRepo.GetInboxAsync(userId);
            return messages.Select(m => ToDto(m));
        }

        public async Task<IEnumerable<MessageDTO>> GetSentAsync(int userId)
        {
            var messages = await _messageRepo.GetSentMessagesAsync(userId);
            return messages.Select(m => ToDto(m));
        }

        public async Task MarkAsReadAsync(int userId, int messageId)
        {
            var message = await _messageRepo.GetByIdAsync(messageId);
            if (message == null) throw new KeyNotFoundException("Message not found.");
            if (message.ToUserId != userId) throw new UnauthorizedAccessException("Only the recipient can mark as read.");

            if (!message.IsRead)
            {
                await _messageRepo.MarkAsReadAsync(messageId);
            }
        }

        public async Task<MessageDTO> SendMessageAsync(int fromUserId, SendMessageDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentException("Message content cannot be empty.");
            if (fromUserId == dto.ToUserId)
                throw new InvalidOperationException("You cannot send a message to yourself.");
            var sender = await _userRepo.GetByIdAsync(fromUserId)
                ?? throw new KeyNotFoundException("Sender not found.");
            var recipient = await _userRepo.GetByIdAsync(dto.ToUserId)
                ?? throw new KeyNotFoundException("Recipient not found.");
            if (sender.Status != UserStatus.Verified)
            {
                var recipientIsAdmin = await _userRepo.UserHasRoleAsync(dto.ToUserId, UserRoleType.Admin);
                if (!recipientIsAdmin)
                    throw new InvalidOperationException("Unverified users may only message Admins.");
            }
            var messageEntity = new Message
            {
                FromUserId = sender.Id,
                ToUserId = recipient.Id,
                Content = dto.Content.Trim(),
                SentAt = DateTime.UtcNow,
                IsRead = false,
                Status = MessageStatus.Sent
            };
            await _messageRepo.AddAsync(messageEntity);
            return new MessageDTO
            {
                Id = messageEntity.Id,
                FromUserId = sender.Id,
                FromUserName_FullName = sender.FullName,
                ToUserId = recipient.Id,
                ToUserName_FullName = recipient.FullName,
                Content = messageEntity.Content,
                IsRead = messageEntity.IsRead,
                SentAt = messageEntity.SentAt
            };
        }

    }
}

