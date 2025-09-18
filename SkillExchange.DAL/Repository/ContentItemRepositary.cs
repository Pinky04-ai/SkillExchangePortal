using Microsoft.EntityFrameworkCore;
using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System.Data.SqlClient;

namespace SkillExchange.DAL.Repository
{
    public class ContentItemRepositary : IContentItem
    {
        private readonly AppDbContext _context;

        public ContentItemRepositary(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ContentItem content)
        {

            _context.ContentItems.Add(content);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveAsync(int contentId)
        {
            var content = await _context.ContentItems.FirstOrDefaultAsync(x => x.Id == contentId);
            if (content != null)
            {
                content.Status = Enums.Enum.ContentStatus.Approved;
                content.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id)
        {
            var content = await _context.ContentItems.FirstOrDefaultAsync(c => c.Id == id);
            if (content != null)
            {
                _context.ContentItems.Remove(content);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<ContentItem>> GetAllAsync()
        {
            return await _context.ContentItems.FromSqlRaw($"EXEC sp_GetAllContentItem").ToListAsync();
        }
        public async Task<IEnumerable<ContentItem>> GetByCategoryAsync(int categoryId, bool onlyApproved = true)
        {
            var result = await _context.ContentItems
            .FromSqlRaw("EXEC sp_GetCategoryByAsync @CategoryId = {0}, @OnlyApproved = {1}", categoryId, onlyApproved)
            .ToListAsync();

            return result;

        }
        public async Task<ContentItem?> GetByIdAsync(int id)
        {
            var result = await _context.ContentItems
                .FromSqlRaw("EXEC sp_GetContentItemById @ContentItemId = {0}", id)
                .Include(c => c.Feedbacks) 
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<ContentItem>> GetByUserAsync(int userId)
        {
            var content = await _context.ContentItems
                     .FromSqlRaw("EXEC sp_GetContentByUser @UserId = {0}", userId)
                     .Include(c => c.Feedbacks)
                     .ToListAsync();

            return content;
        }

        public async Task<IEnumerable<ContentItem>> GetPendingApprovalAsync()
        {
            var content = await _context.ContentItems
           .FromSqlRaw("EXEC sp_GetPendingContent")
           .Include(c => c.Feedbacks) 
           .ToListAsync();

            return content;
        }

        public async Task RejectAsync(int contentId)
        {
            var content = await _context.ContentItems.FirstOrDefaultAsync(c => c.Id == contentId); 
            if (content != null)
            {
                content.Status = Enums.Enum.ContentStatus.Rejected;
                content.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(ContentItem content)
        {
            content.UpdatedAt = DateTime.Now;
            _context.ContentItems.Update(content);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ContentItem>> SearchContentsAsync(
        string? title,
        int? categoryId,
        int? minStars = null,
        bool onlyApproved = true)
        {
            var sql = "EXEC sp_SearchContents @Title, @CategoryId, @MinStars, @OnlyApproved";

            var parameters = new[]
            {
            new SqlParameter("@Title", string.IsNullOrWhiteSpace(title) ? (object)DBNull.Value : title),
            new SqlParameter("@CategoryId", categoryId.HasValue ? (object)categoryId.Value : DBNull.Value),
            new SqlParameter("@MinStars", minStars.HasValue ? (object)minStars.Value : DBNull.Value),
            new SqlParameter("@OnlyApproved", onlyApproved)
        };

            var results = await _context.ContentItems
                .FromSqlRaw(sql, parameters)
                .Include(c => c.User)
                .Include(c => c.Category)
                .Include(c => c.Feedbacks)
                .ToListAsync();

            return results;
        }


    }
}
