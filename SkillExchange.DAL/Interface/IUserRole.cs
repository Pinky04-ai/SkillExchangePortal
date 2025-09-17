using SkillExchange.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Interface
{
    public interface IUserRole
    {
        Task AssignRoleAsync(UserRole userRole);
        Task RemoveRoleAsync(int userId, int roleId);
        Task<UserRole?> GetUserRoleAsync(int userId, int roleId);
        Task<IEnumerable<UserRole>> GetUsersByRoleAsync(int roleId);
        Task<IEnumerable<UserRole>> GetRolesByUserAsync(int userId);

    }
}
