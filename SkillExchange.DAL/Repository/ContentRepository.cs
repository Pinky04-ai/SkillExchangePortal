using Microsoft.EntityFrameworkCore;
using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;
using SkillExchange.DAL.Interfaces;
using SkillExchange.Infrastructure.Data;
using static SkillExchange.Core.Enum.Enum;
namespace SkillExchange.DAL.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly AppDbContext _db;
        public ContentRepository(AppDbContext db) => _db = db;
        public async Task AddAsync(Content content)
        {
            await _db.Contents.AddAsync(content);
            await _db.SaveChangesAsync();
        }
        public async Task<Content?> GetByIdAsync(int id)
        {
            return await _db.Contents
                .Include(c => c.Category)
                .Include(c => c.UploadedBy)
                .Include(c => c.ApprovedBy)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Content>> GetPendingAsync()
        {
            return await _db.Contents
               .Include(c => c.Category)
               .Include(c => c.UploadedBy)
               .Where(c => c.Status == ContentStatus.PendingApproval && !c.IsRejected)
               .OrderBy(c => c.CreatedAt)
               .ToListAsync();
        }
        public async Task<IEnumerable<Content>> GetApprovedContentsAsync()
        {
            return await _db.Contents
                .Include(c => c.Category)
                .Include(c => c.UploadedBy)
                .Where(c => c.Status == ContentStatus.Approved && !c.IsRejected)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
        public async Task<bool> SubmitAsync(Content content)
        {
            await _db.Contents.AddAsync(content);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Content>> GetAllAsync()
        {
            return await _db.Contents
             .Include(c => c.Category)
             .Where(c => c.Status == ContentStatus.Approved && !c.IsRejected)
             .OrderByDescending(c => c.CreatedAt)
             .ToListAsync();
        }
        public async Task UpdateAsync(Content content)
        {
            _db.Contents.Update(content);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var content = await _db.Contents.FindAsync(id);
            if (content == null) return;
            _db.Contents.Remove(content);
            await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<Content>> GetByCategoryAsync(int categoryId)
        {
            return await _db.Contents
          .Include(c => c.Category)
          .Where(c => c.CategoryId == categoryId &&
                      c.Status == ContentStatus.Approved &&
                      !c.IsRejected)
          .OrderByDescending(c => c.CreatedAt)
          .ToListAsync();
        }
        public async Task<IEnumerable<Content>> SearchAsync(string? q, int? categoryId)
        {
            var query = _db.Contents
                 .Include(c => c.Category)
                 .AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(c =>
                    c.Title.Contains(q) || (c.Description != null && c.Description.Contains(q)));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId == categoryId.Value);
            }

            return await query
                .Where(c => c.Status == ContentStatus.Approved && !c.IsRejected)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
        public async Task<bool> ApproveAsync(int contentId, int? approverId, bool approve)
        {
            var content = await _db.Contents.FirstOrDefaultAsync(c => c.Id == contentId);
            if (content == null) return false;

            if (approve)
            {
                content.Status = ContentStatus.Approved;
                content.IsApproved = true;
                content.IsRejected = false;
                content.ApprovedById = approverId;
            }
            else
            {
                content.Status = ContentStatus.Rejected;
                content.IsApproved = false;
                content.IsRejected = true;
                content.ApprovedById = approverId;
            }
            _db.Contents.Update(content);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
