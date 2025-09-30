using Microsoft.AspNetCore.Http;
using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;

namespace SkillExchange.BAL.Interface
{
    public interface IContentManager
    {
        Task<ContentDTO> CreateAsync(CreateContentDTO dto,IFormFile file, string webRootPath);
        Task<ContentDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ContentDTO>> GetAllAsync();
        Task<IEnumerable<ContentDTO>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<ContentDTO>> SearchAsync(string? q, int? categoryId);
        Task ApproveAsync(int contentId, int approverId);
        Task RejectAsync(int contentId, int approverId);
        Task<IEnumerable<ContentDTO>> GetPendingAsync();
        Task<ContentDTO?> GetContentByIdAsync(int contentId);
        Task<bool> UpdateContentStatusAsync(UpdateStatusDTO dto);
        Task<List<ContentDTO>> GetApprovedContentsAsync();
        Task<bool> SubmitContentAsync(Content content);
        Task<bool> UpdateStatusAsync(UpdateStatusDTO dto);
        Task<bool> SubmitFeedbackAsync(FeedbackRequestDTO feedback);
        Task<List<FeedbackDTO>> GetAllFeedbacksForContentAsync(int contentId);
    }
}
