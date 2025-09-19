using SkillExchange.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Interface
{
    public interface IRole
    {
        Task<Role?> GetByIdAsync(int id);
        Task<IEnumerable<Role>> GetAllAsync();
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(int id, Enums.Enum.UserRoleType roleEnum);
        Task<Role?> AssignRoleToUserAsync(int id1, int roleId);
        Task<Role?> GetByTypeAsync(Enums.Enum.UserRoleType user);
        
    }
}
