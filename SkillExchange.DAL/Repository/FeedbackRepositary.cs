using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Repository
{
    public class FeedbackRepositary : IFeedback
    {
        private readonly AppDbContext _context;
        public FeedbackRepositary(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Feedbacks.FindAsync(id);
            if (entity != null)
            {
                _context.Feedbacks.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Feedback>> GetAllByContentAsync(int contentId)
        {
            return await _context.Feedbacks.Where(f => f.ContentId == contentId).ToListAsync();
        }
        
        public async Task<Feedback?> GetByIdAsync(int id)
        {
            return await _context.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);  
        }
        public async Task UpdateAsync(Feedback feedback)
        {
          _context.Feedbacks.Update(feedback);
          await _context.SaveChangesAsync();
        }
    }
}
