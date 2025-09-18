using Microsoft.EntityFrameworkCore;
using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Data.SqlClient;
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
            var sql = "EXEC sp_GetFeedbackByContent @ContentId";
            var param = new SqlParameter("@ContentId", contentId);

            return await _context.Feedbacks
                .FromSqlRaw(sql, param)
                .Include(f => f.User) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetAllByUserAsync(int userId)
        {
            var sql = "EXEC sp_GetFeedbackByUser @UserId";
            var param = new SqlParameter("@UserId", userId);

            return await _context.Feedbacks
                .FromSqlRaw(sql, param)
                .Include(f => f.Content) 
                .ToListAsync();
        }

        public async Task<Feedback?> GetByIdAsync(int id)
        {
            var sql = "EXEC sp_GetFeedbackById @FeedbackId";
            var param = new SqlParameter("@FeedbackId", id);

            return await _context.Feedbacks
                .FromSqlRaw(sql, param)
                .Include(f => f.User)
                .Include(f => f.Content)
                .FirstOrDefaultAsync();
        }
        public async Task UpdateAsync(Feedback feedback)
        {
          _context.Feedbacks.Update(feedback);
          await _context.SaveChangesAsync();
        }

       
    }
}
