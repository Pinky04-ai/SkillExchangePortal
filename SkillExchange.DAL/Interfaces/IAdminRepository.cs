using SkillExchange.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Interfaces
{
    public interface IAdminRepository
    {
        Task<List<Content>> GetPendingContentAsync();
        Task<Content?> GetContentByIdAsync(int id);
        Task UpdateContentAsync(Content content);
        Task AddContentAsync(Content content);

    }
}
