using SkillExchange.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<IEnumerable<Feedback>> GetAllAsync();
        Task<Feedback> GetByIdAsync(int id);
        Task <Feedback>AddAsync(Feedback feedback);
        Task UpdateAsync(Feedback feedback);
        Task DeleteAsync(int id);
        Task<Feedback?> GetByUserAndContentAsync(int userId, int contentId);
        Task<List<Feedback>> GetFeedbacksByContentIdAsync(int contentId);

        Task<List<Feedback>> GetByContentIdAsync(int contentId);
    }
}
