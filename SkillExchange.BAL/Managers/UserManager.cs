
using Microsoft.AspNetCore.Identity;
using SkillExchange.BAL.Interface;
using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;
using SkillExchange.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Linq;
namespace SkillExchange.BAL.Managers
{
     public class UserManager : IUserManager
     {
            private readonly IUserRepository _userRepo;
            private readonly PasswordHasher<User> _hasher;
            public UserManager(IUserRepository userRepo)
            {
                 _userRepo = userRepo;
                 _hasher = new PasswordHasher<User>();
            }
            public async Task<UserDTO?> RegisterAsync(RegisterDTO dto)
            {
                var existing = await _userRepo.GetByEmailAsync(dto.Email);
                if (existing != null) return null;
                var user = new User
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Status = Core.Enum.Enum.UserStatus.UnderVerification,
                    Role = Core.Enum.Enum.UserRole.User,
                    CreatedAt = DateTime.UtcNow
                };
                user.PasswordHash = _hasher.HashPassword(user, dto.Password);
                await _userRepo.AddAsync(user);
                return new UserDTO(user.Id, $"{user.FirstName} {user.LastName}", user.Email, user.Role.ToString(), user.Status.ToString());
            }
            public async Task<UserDTO?> LoginAsync(LoginDTO dto)
            {
                var user = await _userRepo.GetByEmailAsync(dto.Email);
                if (user == null) return null;
                var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
                if (result == PasswordVerificationResult.Failed) return null;
                return new UserDTO(user.Id, $"{user.FirstName} {user.LastName}", user.Email, user.ToString(), user.Status.ToString());
            }
            public async Task ApproveUserAsync(int userId)
            {
                var user = await _userRepo.GetByIdAsync(userId);
                if (user == null) return;
                user.Status = Core.Enum.Enum.UserStatus.Verified;
                user.VerifiedAt = DateTime.UtcNow;
                await _userRepo.UpdateAsync(user);
            }
            public async Task<UserDTO?> GetByIdAsync(int id)
            {
                var user = await _userRepo.GetByIdAsync(id);
                if (user == null) return null;
                return new UserDTO(user.Id, $"{user.FirstName} {user.LastName}", user.Email, user.Role.ToString(), user.Status.ToString());
            }
            public async Task<IEnumerable<UserDTO>> GetAllAsync()
            {
                var users = await _userRepo.GetAllAsync();
                return users.Select(u => new UserDTO(u.Id, $"{u.FirstName} {u.LastName}", u.Email, u.Role.ToString(), u.Status.ToString()));
            }
     }
}

