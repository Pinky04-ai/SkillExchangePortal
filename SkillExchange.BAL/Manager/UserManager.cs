
using Microsoft.Extensions.Logging;
using SkillExchange.API.DTO.AppUser;
using SkillExchange.BAL.Interfaces;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using static SkillExchange.DAL.Enums.Enum;

namespace SkillExchange.BAL.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IAppUser _userRepo;
        private readonly IRole _roleRepo;
        private readonly ILogger<UserManager> _logger;

        public UserManager(IAppUser userRepo, IRole roleRepo, ILogger<UserManager> logger)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _logger = logger;
        }
        public async Task AssignRoleAsync(int userId, DAL.Enums.Enum.UserRoleType role)
        {
            var roleEntity = await _roleRepo.GetByTypeAsync(role);
            if (roleEntity == null)
                throw new KeyNotFoundException("Role not found.");

            await _roleRepo.AssignRoleToUserAsync(userId, roleEntity.Id);
            _logger.LogInformation("Role {Role} assigned to user {UserId}", role, userId);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Select(u => new UserDTO
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                IsVerified = u.Status == UserStatus.Verified,
                CreatedAt = u.CreatedAt
            });
        }

        public async Task<IEnumerable<UserDTO>> GetUnverifiedAsync()
        {
            var users = await _userRepo.GetUnverifiedUsersAsync();
            return users.Select(u => new UserDTO
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                IsVerified = u.Status == UserStatus.Verified,
                CreatedAt = u.CreatedAt
            });
        }

        public async Task<UserDTO?> LoginAsync(LoginUserDTO dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null || user.Password != dto.Password) 
                return null;

            if (user.Status != UserStatus.Verified)
                throw new InvalidOperationException("Account not verified by admin.");

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                IsVerified = user.Status == UserStatus.Verified,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserDTO> RegisterAsync(RegisterUserDTO dto)
        {
            var existing = await _userRepo.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new InvalidOperationException("Email already registered.");
            var user = new AppUser
            {
                Email = dto.Email.Trim(),
                Password = dto.Password,
                FullName = dto.FullName.Trim(),
                Status = UserStatus.UnderVerification,
                CreatedAt = DateTime.UtcNow
            };
            await _userRepo.AddAsync(user);
            var role = await _roleRepo.GetByTypeAsync(UserRoleType.User);
            if (role != null)
            {
                await _roleRepo.AssignRoleToUserAsync(user.Id, role.Id);
            }
            _logger.LogInformation("User registered: {Email}", user.Email);
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                IsVerified = user.Status == UserStatus.Verified,
                CreatedAt = user.CreatedAt
            };
        }
        public async Task VerifyUserAsync(int userId)
        {
             await _userRepo.VerifyUserAsync(userId);
            _logger.LogInformation("User {UserId} verified.", userId);
        }
    }
}
