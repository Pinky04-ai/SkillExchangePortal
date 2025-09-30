using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.Core.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime? VerifiedAt { get; set; }
        
        public UserDTO() { }
        public UserDTO(int id, string fullName, string email, string role, string status)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Role = role;
            Status = status;
        }
        public UserDTO(int id , string Email,string Role,DateTime? VerifiedAt, string status)
        {
            Id= id;
            Email = Email;
            Role = Role;
            Status = status;
            VerifiedAt = VerifiedAt;
        }
    }
}
