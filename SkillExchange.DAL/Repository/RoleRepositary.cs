using Microsoft.EntityFrameworkCore;
using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System.Data.SqlClient;

//using System.Data.Entity;
using static SkillExchange.DAL.Enums.Enum;

namespace SkillExchange.DAL.Repository
{
    public class RoleRepositary : IRole
    {
        private readonly AppDbContext _context;
        public RoleRepositary(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.Roles.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            var sql = "EXEC sp_GetAllRoles";

            var results = await _context.Roles
                .FromSqlRaw(sql)
                .Include(r => r.UserRoles) 
                .ToListAsync();

            return results;
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            var sql = "EXEC sp_GetRoleById @RoleId";
            var parameter = new SqlParameter("@RoleId", id);

            var role = await _context.Roles
                .FromSqlRaw(sql, parameter)
                .Include(r => r.UserRoles)
                .FirstOrDefaultAsync();

            return role;
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
        public async Task<Role?> GetByTypeAsync(UserRoleType role)
        {
            var sql = "EXEC sp_GetRoleByType @RoleName";
            var parameter = new SqlParameter("@RoleName", role.ToString());

            return await _context.Roles
                .FromSqlRaw(sql, parameter)
                .FirstOrDefaultAsync();
        }
        public async Task<Role?> AssignRoleToUserAsync(int userId, object roleId)
        {
            if (roleId == null) return null;
            int rId = Convert.ToInt32(roleId);
            var role = await _context.Roles.FindAsync(rId);
            if (role == null) return null;
            var exists = await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == rId);
            if (!exists)
            {
                await _context.UserRoles.AddAsync(new UserRole
                {
                    UserId = userId,
                    RoleId = rId
                });
                await _context.SaveChangesAsync();
            }
            return role;
        }

    }
}
