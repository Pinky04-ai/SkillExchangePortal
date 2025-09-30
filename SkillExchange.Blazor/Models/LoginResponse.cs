namespace SkillExchange.Blazor.Models
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token {  get; set; }= string.Empty;
        public int Id { get; set; }
    }
}   
