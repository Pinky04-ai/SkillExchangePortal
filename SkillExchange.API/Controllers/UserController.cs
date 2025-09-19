using Microsoft.AspNetCore.Mvc;
using SkillExchange.API.DTO.AppUser;
using SkillExchange.API.DTO.UserRole;
using SkillExchange.BAL.Interfaces;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using static SkillExchange.DAL.Enums.Enum;

namespace SkillExchange.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IAppUser _userRepo;
        private readonly IRole _roleRepo;

        public UsersController(IUserManager userManager, IAppUser userRepo, IRole roleRepo)
        {
            _userManager = userManager;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _userManager.RegisterAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userDto = await _userManager.LoginAsync(model);
            if (userDto == null) return Unauthorized(new { error = "Invalid credentials." });

            return Ok(new
            {
                user = userDto
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _userManager.GetAllAsync());
        [HttpGet("unverified")]
        public async Task<IActionResult> GetUnverified() => Ok(await _userManager.GetUnverifiedAsync());
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(new UserDTO
            {
                Id = user.Id,
                Email = user.Email!,
                FullName = user.FullName,
                IsVerified = user.Status == UserStatus.Verified,
                CreatedAt = user.CreatedAt,
                Roles = user.UserRoles?.Select(r => r.Role.RoleName)
            });
        }

        [HttpPost("{id:int}/verify")]
        public async Task<IActionResult> VerifyUser(int id)
        {
            await _userManager.VerifyUserAsync(id);
            return Ok(new { message = "User verified." });
        }

        [HttpPost("{id:int}/assign-role")]
        public async Task<IActionResult> AssignRole(int id, AssignRoleDTO dto)
        {
            await _userManager.AssignRoleAsync(id, (UserRoleType)dto.RoleName);
            return Ok($"Role '{dto.RoleName}' assigned to user {id}.");
        }
        //[HttpDelete("{id:int}/roles/{roleName}")]
        //public async Task<IActionResult> RemoveRole(int id, string roleName)
        //{
        //    await _roleRepo.DeleteAsync(id);
        //    return Ok($"Role '{roleName}' removed from user {id}.");
        //}
    }
}
