using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static SkillExchange.Core.Enum.Enum;

namespace SkillExchange.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; } = UserStatus.UnderVerification;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? VerifiedAt { get; set; }
        public ICollection<Content> UploadedContents { get; set; } = new List<Content>();
        public ICollection<Content> ApprovedContents { get; set; } = new List<Content>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
