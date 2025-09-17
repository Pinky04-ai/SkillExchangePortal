using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System.Data.Entity;
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
            return await _context.Messages.Include(x => x.FromUser)
                                    .Include(x=>x.ToUser)
                                    .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<Message>> GetInboxAsync(int userId)
        {
            return await _context.Messages.Include(x => x.FromUser)
                                    .Include(x => x.ToUser)
                                    .Where(x => x.ToUserId == userId)
                                    .OrderByDescending(x => x.SentAt).ToListAsync();
        }
        public async Task<IEnumerable<Message>> GetMessageBetweenUserAsync(int fromUserId, int toUserId)
        {
            return await _context.Messages
                            .Include(m => m.FromUser)
                            .Include(m => m.ToUser)
                            .Where(m =>
                                (m.FromUserId == fromUserId && m.ToUserId == toUserId) ||
                                (m.FromUserId == toUserId && m.ToUserId == fromUserId)
                            )
                            .OrderBy(m => m.SentAt)
                            .ToListAsync();
        }
        public async Task<IEnumerable<Message>> GetSentMessagesAsync(int userId)
        {
            return await _context.Messages
                           .Include(m => m.FromUser)
                           .Include(m => m.ToUser)
                           .Where(m => m.FromUserId == userId)
                           .OrderByDescending(m => m.SentAt)
                           .ToListAsync();
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
