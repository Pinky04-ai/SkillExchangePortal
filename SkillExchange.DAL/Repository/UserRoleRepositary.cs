using Microsoft.EntityFrameworkCore;
using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System.Data.SqlClient;

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
            var sql = "EXEC sp_GetRolesByUser @UserId";
            var parameter = new SqlParameter("@UserId", userId);

            return await _context.UserRoles
                .FromSqlRaw(sql, parameter)
                .Include(ur => ur.Role)
                .ToListAsync();
        }
        public async Task<IEnumerable<UserRole>> GetUsersByRoleAsync(int roleId)
        {
            var sql = "EXEC sp_GetUsersByRoles @RoleId";
            var parameter = new SqlParameter("@RoleId", roleId);
            return await _context.UserRoles
                .FromSqlRaw(sql, parameter)
                .Include(ur => ur.User) 
                .ToListAsync();
        }
        public async Task<UserRole?> GetUserRoleAsync(int userId, int roleId)
        {
            var sql = "EXEC sp_GetUserRole @UserId, @RoleId";
            var parameters = new[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@RoleId", roleId)
            };
            return await _context.UserRoles
                .FromSqlRaw(sql, parameters)
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync();
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
