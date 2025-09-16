using System.Security.Permissions;

namespace SkillExchange.API.DTO.AppUser
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email {  get; set; }
        public string FullName {  get; set; }
        public bool IsVerfied {  get; set; }
    }
}
