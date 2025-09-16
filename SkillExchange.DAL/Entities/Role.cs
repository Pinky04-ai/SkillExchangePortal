using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkillExchange.DAL.Enums.Enum;

namespace SkillExchange.DAL.Entities
{
    public class Role
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Role name is required.")]
        [MaxLength(100, ErrorMessage = "Role name cannot exceed 100 characters.")]
        public string? RoleName { get; set; }
        [Required]
        [EnumDataType(typeof(UserRoleType))]
        public UserRoleType RoleType { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<UserRole> UserRoles { get; set; }
    }

}
