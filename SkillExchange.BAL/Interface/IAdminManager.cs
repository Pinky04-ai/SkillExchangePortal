using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;
namespace SkillExchange.BAL.Interface
{
    public interface IAdminManager
    {
        Task<List<ContentDTO>> GetPendingContentAsync();
        Task<bool> ApproveContentAsync(int contentId);
        Task<bool> RejectContentAsync(int contentId);
        Task AddContentAsync(Content content); 
    }
}
