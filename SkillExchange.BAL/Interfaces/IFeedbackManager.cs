using SkillExchange.API.DTO.Feedback;
namespace SkillExchange.BAL.Interfaces
{
    public interface IFeedbackManager
    {
        Task<FeedbackDTO?> GetByIdAsync(int id);
        Task<IEnumerable<FeedbackDTO>> GetAllByContentAsync(int contentId);
        Task<IEnumerable<FeedbackDTO>> GetAllByUserAsync(int userId);
        Task<FeedbackDTO> AddAsync(int userId, CreateFeedbackDTO dto);
        Task<bool> UpdateAsync(int id, CreateFeedbackDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
