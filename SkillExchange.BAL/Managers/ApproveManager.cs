using Microsoft.EntityFrameworkCore;
using SkillExchange.BAL.Interface;
using SkillExchange.Core.DTO;
using SkillExchange.Infrastructure.Data;
using static SkillExchange.Core.Enum.Enum;

namespace SkillExchange.BAL.Managers
{
    public class ApproveManager : IApproveManager
    {
        private readonly AppDbContext _context;

        public ApproveManager(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ApproveDTO>> GetPendingUsers()
        {
            return await _context.Users
                .Where(u => u.VerifiedAt == null && u.Status == UserStatus.UnderVerification)
                .Select(u => new ApproveDTO
                {
                    Id = u.Id,
                    Email = u.Email,
                    Role = u.Role.ToString(),
                    Status = u.Status.ToString(),
                    VerifiedAt = u.VerifiedAt
                })
                .ToListAsync();
        }
        public async Task<bool> ApproveUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.VerifiedAt = DateTime.UtcNow;
            user.Status = UserStatus.Verified;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RejectUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.Status = UserStatus.Rejected;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
