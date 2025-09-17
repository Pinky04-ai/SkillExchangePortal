using Microsoft.EntityFrameworkCore;
using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;

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
            return await _context.ContentItems.Include(c => c.User).Include(c => c.Category).Include(c => c.Feedbacks).ToListAsync();
        }
        public async Task<IEnumerable<ContentItem>> GetByCategoryAsync(int categoryId, bool onlyApproved = true)
        {
            var q = _context.ContentItems
              .Include(c => c.User)
              .Include(c => c.Category)
              .Include(c => c.Feedbacks)
              .Where(c => c.CategoryId == categoryId);

            if (onlyApproved)
                q = q.Where(c => c.Status == Enums.Enum.ContentStatus.Approved);

            return await q.ToListAsync();

        }

        public async Task<ContentItem?> GetByIdAsync(int id)
        {
            return await _context.ContentItems.Include(c => c.User)
                                        .Include(c => c.Category)
                                        .Include(c => c.Feedbacks)
                                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<ContentItem>> GetByUserAsync(int userId)
        {
            return await _context.ContentItems.Include(c => c.Category)
                                        .Include(c =>c.Feedbacks)
                                        .Where(c => c.UserId == userId)
                                        .ToListAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetPendingApprovalAsync()
        {
            return await _context.ContentItems.Include(c => c.User)
                                        .Include(c => c.Category)
                                        .Include(c => c.Feedbacks)
                                        .Where(c => c.Status == Enums.Enum.ContentStatus.PendingApproval)
                                        .ToListAsync();
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
             int? page = null,
             int? pageSize = null,
             bool onlyApproved = true)
        {
            IQueryable<ContentItem> query = _context.ContentItems
                .Include(c => c.Category)
                .Include(c => c.User)
                .Include(c => c.Feedbacks);

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(c => EF.Functions.Like(c.Title, $"%{title}%"));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId == categoryId.Value);
            }
            if (onlyApproved)
            {
                query = query.Where(c => c.Status == Enums.Enum.ContentStatus.Approved);
            }
            if (minStars.HasValue)
            {
                if (typeof(ContentItem).GetProperty("AverageStars") != null)
                {
                    query = query.Where(c => c.Stars.HasValue && c.Stars >= minStars.Value);
                }
                else
                {
                    query = query.Where(c => c.Feedbacks.Any())
                                 .Where(c => c.Feedbacks.Average(f => (double?)f.Rating) >= (double)minStars.Value);
                }
            }
            if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
            {
                int skip = ((int)page - 1) * (int)pageSize;
                query = query.Skip(skip).Take((int)pageSize);
            }
            var result = await query.ToListAsync();
            return result;
        }

    }
}
