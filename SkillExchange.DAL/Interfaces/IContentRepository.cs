using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkillExchange.Core.Enum.Enum;

namespace SkillExchange.DAL.Interfaces
{
    public interface IContentRepository
    {
        Task<Content?> GetByIdAsync(int id);
        Task<IEnumerable<Content>> GetAllAsync();
        Task<IEnumerable<Content>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Content>> SearchAsync(string? q, int? categoryId);
        Task<bool> SubmitAsync(Content content);
        Task AddAsync(Content content);
        Task<IEnumerable<Content>> GetPendingAsync();
        Task<IEnumerable<Content>> GetApprovedContentsAsync();
        Task UpdateAsync(Content content);
        Task DeleteAsync(int id);
        Task<bool> ApproveAsync(int contentId, int?approverId,bool approve);
    }
}
