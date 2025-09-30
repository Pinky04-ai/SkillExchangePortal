using SkillExchange.Core.DTO;

namespace SkillExchange.BAL.Interface
{
    public interface IJwtService
    {
        string GenerateToken(UserDTO user);
    }
}
