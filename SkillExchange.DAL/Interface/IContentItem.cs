using SkillExchange.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Interface
{
    public interface IContentItem
    {
        Task<ContentItem?> GetByIdAsync(int id);
        Task<IEnumerable<ContentItem>> GetAllAsync();
        Task<IEnumerable<ContentItem>> GetByCategoryAsync(int categoryId, bool onlyApproved = true);
        Task<IEnumerable<ContentItem>> GetByUserAsync(int userId);
        Task<IEnumerable<ContentItem>> GetPendingApprovalAsync();

        Task AddAsync(ContentItem content);
        Task UpdateAsync(ContentItem content);
        Task DeleteAsync(int id);

        Task ApproveAsync(int contentId);
        Task RejectAsync(int contentId);
        Task<IEnumerable<ContentItem>> SearchContentsAsync(
           string? title,
           int? categoryId,
           int? minStars = null,
           int? page = null,
           int? pageSize = null,
           bool onlyApproved = true);

    }
}
