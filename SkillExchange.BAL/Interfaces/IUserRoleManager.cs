using SkillExchange.API.DTO.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.BAL.Interfaces
{
    public interface IUserRoleManager
    {
        Task AssignRolesAsyc(AssignRoleDTO dto);
        Task RemoveRoleAsync(int userId, int roleId);
        Task <UserRoleDTO?>GetUserRoleAsync(int userId,int roleId);
        Task<IEnumerable<UserRoleDTO>> GetRolesByUserAsync(int userId);
        Task<IEnumerable<UserRoleDTO>> GetUsersByRoleAsync(int roleId);
    }
}
