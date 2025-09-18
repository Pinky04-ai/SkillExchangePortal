using SkillExchange.API.DTO.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkillExchange.DAL.Enums.Enum;

namespace SkillExchange.BAL.Interfaces
{
    public interface IUserManager
    {
        Task<UserDTO> RegisterAsync(RegisterUserDTO dto);
        Task<UserDTO?> LoginAsync(LoginUserDTO dto);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<IEnumerable<UserDTO>> GetUnverifiedAsync();
        Task VerifyUserAsync(int userId);
        Task AssignRoleAsync(int userId, UserRoleType role);
    }
}
