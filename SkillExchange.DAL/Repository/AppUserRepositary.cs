using SkillExchange.DAL.Database;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System.Data.Entity;
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
            var users = await _context.Users.ToListAsync();
            foreach (var user in users)
            {
                await _context.Entry(user)
                              .Collection(u => u.UserRoles)
                              .Query()
                              .Include(ur => ur.Role)
                              .LoadAsync();
            }

            return users;
        }

        public async Task<AppUser>? GetByEmailAsync(string email)
        {
            var user = await _context.Users
                        .Include(u => u.UserRoles)
                        .FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                await _context.Entry(user)
                    .Collection(u => u.UserRoles)
                    .Query()
                    .Include(ur => ur.Role)
                    .LoadAsync();
            }

            return user;
        }

        public async Task<AppUser>? GetByIdAsync(int id)
        {
            return await _context.Users
             .Include(u => u.UserRoles)         
             .Include(u => u.Contents)
             .Include(u => u.Feedbacks)
             .Include(u => u.SentMessages)
             .Include(u => u.ReceivedMessages)
             .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<AppUser>> GetUnverifiedUsersAsync()
        {
            return await _context.Users
                   .Where(u => u.Status == UserStatus.UnderVerification)
                   .ToListAsync();
        }

        public async Task UpdateAsync(AppUser user)
        {
             _context.Users.Update(user);
            await _context.SaveChangesAsync();
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
