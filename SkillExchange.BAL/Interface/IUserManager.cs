using SkillExchange.Core.DTO;

namespace SkillExchange.BAL.Interface
{
    public interface IUserManager
    {
        Task<UserDTO?> RegisterAsync(RegisterDTO dto);
        Task<UserDTO> LoginAsync(LoginDTO dto); 
        Task ApproveUserAsync(int userId);
        Task<UserDTO?> GetByIdAsync(int userId);
        Task<IEnumerable<UserDTO>> GetAllAsync();
    }
}
