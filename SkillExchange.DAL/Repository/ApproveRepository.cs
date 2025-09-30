using Microsoft.EntityFrameworkCore;
using SkillExchange.Core.Entities;
using SkillExchange.DAL.Interfaces;
using SkillExchange.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Repository
{
    public class ApproveRepository : IApproveRepository
    {
        private readonly AppDbContext _context;

        public ApproveRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetPendingUsersAsync()
        {
            return await _context.Users
                .Where(u => u.VerifiedAt == null && u.Status == Core.Enum.Enum.UserStatus.UnderVerification)
                .ToListAsync();
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
