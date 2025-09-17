using SkillExchange.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Interface
{
    public  interface IAppUser
    {
        Task<AppUser?> GetByIdAsync(int id);
        Task<AppUser?> GetByEmailAsync(string email);
        Task<IEnumerable<AppUser>> GetAllAsync();
        Task<IEnumerable<AppUser>> GetUnverifiedUsersAsync();

        Task AddAsync(AppUser user);
        Task UpdateAsync(AppUser user);
        Task DeleteAsync(int id);
        Task VerifyUserAsync(int userId);
    }
}
