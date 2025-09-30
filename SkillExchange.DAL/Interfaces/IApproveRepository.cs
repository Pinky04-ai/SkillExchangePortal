using SkillExchange.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Interfaces
{
    public interface IApproveRepository
    {
        Task<List<User>> GetPendingUsersAsync();
        Task<User>GetUserByIdAsync(int id);
        Task UpdateUserAsync(User user);
    }
}
