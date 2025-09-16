using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static SkillExchange.DAL.Enums.Enum;

namespace SkillExchange.DAL.Entities
{

    public class AppUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(200)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8,
        ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must include uppercase, lowercase, number, and special character.")]
        public string? Password { get; set; }
        [Required]
        [MaxLength(200)]
        [RegularExpression(@"^[a-zA-Z\s]+$",
        ErrorMessage = "Full name can only contain letters and spaces.")]
        public string? FullName { get; set; }
        [Required]
        public UserStatus Status { get; set; } = UserStatus.UnderVerification;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<ContentItem> Contents { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }

}
