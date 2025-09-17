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
            return await _context.Roles.Include(r => r.UserRoles).ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles.Include(r => r.UserRoles).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
    }
}
