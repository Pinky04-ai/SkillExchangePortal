using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Repository
{
    public class UserRoleRepositary : IUserRole
    {
        private readonly AppDbContext _context;

        public UserRoleRepositary(AppDbContext context)
        {
            _context = context;
        }
        public async Task AssignRoleAsync(UserRole userRole)
        {
            var exists = await _context.UserRoles.AnyAsync(u => u.UserId == userRole.UserId && u.RoleId == userRole.RoleId);
            if (!exists)
            {
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserRole>> GetRolesByUserAsync(int userId)
        {
            return await _context.UserRoles.Include(u => u.Role).Where(u => u.UserId == userId).ToListAsync();   
        }

        public async Task<IEnumerable<UserRole>> GetUsersByRoleAsync(int roleId)
        {
           return await _context.UserRoles.Include(u=>u.User).Where(u => u.RoleId==roleId).ToListAsync();
        }

        public async Task<UserRole?> GetUserRoleAsync(int userId, int roleId)
        {
            return await _context.UserRoles.Include(u => u.Role).Include(u => u.User).FirstOrDefaultAsync(u => u.UserId == userId && u.RoleId == roleId);
        }

        public async Task RemoveRoleAsync(int userId, int roleId)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == userId && u.RoleId == roleId);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
