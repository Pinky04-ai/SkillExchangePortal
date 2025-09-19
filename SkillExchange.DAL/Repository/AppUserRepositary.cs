using Microsoft.EntityFrameworkCore;
using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
//using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using static SkillExchange.DAL.Enums.Enum;

namespace SkillExchange.DAL.Repository
{
    public class AppUserRepositary : IAppUser
    {
        private readonly AppDbContext _context;

        public AppUserRepositary(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(AppUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            var sql = "EXEC sp_GetAllUsersWithRoles";
            var userRolesData = await _context.Users
                .FromSqlRaw(sql)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            return userRolesData;
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            var param = new SqlParameter("@Email", email);
            var result =  _context.Users
                .FromSqlRaw("EXEC sp_GetUserByEmail @Email", param)
                .AsEnumerable()
                .FirstOrDefault();
            return result;
        }

        public async Task<AppUser>? GetByIdAsync(int id)
        {
            var list = _context.Users
            .FromSqlRaw("EXEC sp_GetUserById @UserId", new SqlParameter("@UserId", id))
            .AsEnumerable() 
            .ToList();
            return list.SingleOrDefault();
        }

        public async Task<IEnumerable<AppUser>> GetUnverifiedUsersAsync()
        {
            return await _context.Users
                        .FromSqlRaw("EXEC sp_GetUnverifiedUsers")
                        .ToListAsync();
        }

        public async Task UpdateAsync(AppUser user)
        {
             _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserHasRoleAsync(int userId, UserRoleType role)
        {
            return await _context.UserRoles
                        .Include(ur => ur.Role)
                        .AnyAsync(ur => ur.UserId == userId && ur.Role.RoleName == role.ToString());
        }

        public async Task VerifyUserAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.Status = UserStatus.Verified;
                await _context.SaveChangesAsync();
            }
        }
    }
}
