using Microsoft.EntityFrameworkCore;
using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;

using Microsoft.Data.SqlClient;

using System.Runtime.CompilerServices;

namespace SkillExchange.DAL.Repository
{
    public class MessageRepositary : IMessage
    {
        private readonly AppDbContext _context;
        public MessageRepositary(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Messages.FindAsync(id);
            if(entity != null)
            {
                _context.Messages.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Message?> GetByIdAsync(int id)
        {
            var sql = "EXEC sp_GetMessageById @MessageId";
            var param = new SqlParameter("@MessageId", id);
            var result = await _context.Messages
                .FromSqlRaw(sql, param)
                .FirstOrDefaultAsync();

            return result;
        }

        public Task<IEnumerable<Message>> GetInboxAsync(int userId)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<Message>> GetInboxAsync(int userId)
        //{
        //    var sql = "EXEC sp_GetInboxMessage @UserId";
        //    var param = new SqlParameter("@UserId", userId);

        //    var messages = await _context.Set<InboxMessageDTO>()
        //        .FromSqlRaw(sql, param)
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return messages;
        //}
        public async Task<IEnumerable<Message>> GetMessageBetweenUserAsync(int fromUserId, int toUserId)
        {
            var sql = "EXEC sp_GetMessageBetweenUser @FromUserId, @ToUserId";

            var parameters = new[]
            {
                new SqlParameter("@FromUserId", fromUserId),
                new SqlParameter("@ToUserId", toUserId)
            };

            var results = await _context.Messages
                .FromSqlRaw(sql, parameters)
                .ToListAsync();

            return results;
        }
        public async Task<IEnumerable<Message>> GetSentMessagesAsync(int userId)
        {
            var sql = "EXEC sp_GetSentMessages @UserId";

            var parameter = new SqlParameter("@UserId", userId);

            var results = await _context.Messages
                .FromSqlRaw(sql, parameter)
                .ToListAsync();

            return results;
        }
        public async Task MarkAsReadAsync(int messageId)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
            if (message != null)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
