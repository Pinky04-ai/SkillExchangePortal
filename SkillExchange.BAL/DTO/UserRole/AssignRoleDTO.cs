
namespace SkillExchange.API.DTO.UserRole
{
    public class AssignRoleDTO
    {
        public readonly object RoleName;

        public int UserId {  get; set; }
        public int RoleId { get; set; }
       
    }
}
