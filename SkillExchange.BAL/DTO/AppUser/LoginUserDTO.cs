using System.ComponentModel.DataAnnotations;

namespace SkillExchange.API.DTO.AppUser
{
    public class LoginUserDTO
    {
        [Required, EmailAddress]
        public string? Email {  get; set; }
        [Required, MinLength(8)]
        public string? Password { get; set; }

    }
}
