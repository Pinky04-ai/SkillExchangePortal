using System.ComponentModel.DataAnnotations;

namespace SkillExchange.API.DTO.AppUser
{
    public class RegisterUserDTO
    {
        [Required, EmailAddress]
        public string? Email {  get; set; }
        [Required, MinLength(8)]
        public string? Password { get; set; }
        [Required, MaxLength(200)]
        public string? FullName {  get; set; }

    }
}
