using SkillExchange.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Interface
{
    public interface IFeedback
    {
        Task<Feedback?> GetByIdAsync(int id);
        Task<IEnumerable<Feedback>> GetAllByContentAsync(int contentId);

        Task AddAsync(Feedback feedback);
        Task UpdateAsync(Feedback feedback);
        Task DeleteAsync(int id);
    }
}
