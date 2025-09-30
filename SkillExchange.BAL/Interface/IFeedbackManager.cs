using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.BAL.Interface
{
    public interface IFeedbackManager
    {
        Task<IEnumerable<FeedbackDTO>> GetAllAsync();
        //Task<IEnumerable<Feedback>> GetAllAsync();
        Task<Feedback> GetByIdAsync(int id);
        Task<FeedbackDTO> AddAsync(CreateFeedbackDTO dto);
        Task<FeedbackDTO?> UpdateAsync(int id, CreateFeedbackDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<ContentDTO> GetContentByIdAsync(int contentId);
        Task<bool> SubmitFeedbackAsync(FeedbackRequestDTO feedback);
    }
}
