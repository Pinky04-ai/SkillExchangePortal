using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System.Data.Entity;

namespace SkillExchange.DAL.Repository
{
    public class MessageRepositary : IMessage
    {
        private readonly AppDbContext _context;
        public MessageRepositary(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var entity = _context.Messages.Find(id);
            if(entity != null)
            {
                _context.Messages.Remove(entity);
                _context.SaveChanges();
            }
        }
        public Message? GetById(int id)
        {
            return _context.Messages.Include(x => x.FromUser)
                                    .Include(x=>x.ToUser)
                                    .FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Message> GetInbox(int userId)
        {
            return _context.Messages.Include(x => x.FromUser)
                                    .Include(x => x.ToUser)
                                    .Where(x => x.ToUserId == userId)
                                    .OrderByDescending(x => x.SentAt).ToList();
        }
        public IEnumerable<Message> GetMessageBetweenUser(int fromUserId, int toUserId)
        {
            return _context.Messages
                            .Include(m => m.FromUser)
                            .Include(m => m.ToUser)
                            .Where(m =>
                                (m.FromUserId == fromUserId && m.ToUserId == toUserId) ||
                                (m.FromUserId == toUserId && m.ToUserId == fromUserId)
                            )
                            .OrderBy(m => m.SentAt)
                            .ToList();
        }
        public IEnumerable<Message> GetSentMessages(int userId)
        {
            return _context.Messages
                           .Include(m => m.FromUser)
                           .Include(m => m.ToUser)
                           .Where(m => m.FromUserId == userId)
                           .OrderByDescending(m => m.SentAt)
                           .ToList();
        }
        public void MarkAsRead(int messageId)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == messageId);
            if (message != null)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }
        }
    }
}
