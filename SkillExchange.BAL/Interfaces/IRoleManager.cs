using SkillExchange.API.DTO.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.BAL.Interfaces
{
    public interface IRoleManager
    {
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO?> GetRoleByIdAsync(int id);
        Task<RoleDTO> CreateRoleAsync(CreateRoleDTO dto);
        Task<RoleDTO?> UpdateRoleAsync(UpdateRoleDTO dto);
        Task<bool> DeleteRoleAsync(int id);
        Task AssignRoleToUserAsync(int userId, int roleId);
        Task RemoveRoleFromUserAsync(int userId, int roleId);
    }
}
