using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillExchange.BAL.Interface;
using SkillExchange.Blazor.Models;
using SkillExchange.Core.DTO;
namespace SkillExchange.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class AuthController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IJwtService _jwtService;
        public AuthController(IUserManager userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromBody] RegisterDTO dto)
        {
            var user = await _userManager.RegisterAsync(dto);
            if (user == null)
                return BadRequest("Email already exists.");
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody] LoginDTO dto)
        {
            var user = await _userManager.LoginAsync(dto);
            if (user == null) return Unauthorized("Invalid credentials.");
            var token = _jwtService.GenerateToken(new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString()
            });
            var response = new LoginResponse
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString(),
                Token = token
            };

            return Ok(response);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await _userManager.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userManager.GetAllAsync();
            return Ok(users);
        }
        [HttpPost("approve/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveUser(int id)
        {
            await _userManager.ApproveUserAsync(id);
            return NoContent();
        }

    }
}
