
using Microsoft.EntityFrameworkCore;
using SkillExchange.Core.Entities;
using SkillExchange.DAL.Interfaces;
using SkillExchange.Infrastructure.Data;

namespace SkillExchange.DAL.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext _context;

        public FeedbackRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Feedback>> GetAllAsync()
        {
            return await _context.Feedbacks
                .FromSqlRaw("EXEC sp_GetAllFeedbacks")
                .AsNoTracking()
                .ToListAsync();


        }
        public async Task<Feedback> GetByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var feedbacks = await _context.Feedbacks
                .FromSqlInterpolated($"EXEC sp_GetFeedbackById @FeedbackId = {id}")
                .AsNoTracking()
                .ToListAsync();

            return feedbacks.FirstOrDefault();
        }
        public async Task<Feedback> AddAsync(Feedback feedback)
        {
            await _context.Database.ExecuteSqlRawAsync(
          "EXEC sp_AddFeedback @UserId={0}, @ContentId={1}, @Rating={2}, @Comment={3}",
          feedback.UserId, feedback.ContentId, feedback.Rating, feedback.Comment);
              
            return feedback;
        }
        public async Task<List<Feedback>> GetByContentIdAsync(int contentId)
        {
            return await _context.Feedbacks
         .Where(f => f.ContentId == contentId)
         .OrderByDescending(f => f.CreatedAt)
         .ToListAsync();
        }
        public async Task UpdateAsync(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
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
        public async Task<Feedback?> GetByUserAndContentAsync(int userId, int contentId)
        {
            return await _context.Feedbacks
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ContentId == contentId);
        }
        public async Task<List<Feedback>> GetFeedbacksByContentIdAsync(int contentId)
        {
            return await _context.Feedbacks
                .Where(f => f.ContentId == contentId)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }
    }
}
