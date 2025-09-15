using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Entities
{
    public enum UserStatus { UnderVerification = 0, Verified = 1, Blocked = 2 }
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public UserStatus Status { get; set; } = UserStatus.UnderVerification;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<ContentItem> Contents { get; set; } 
        public ICollection<Feedback> Feedbacks { get; set; } 
        public ICollection<Message> SentMessages { get; set; } 
        public ICollection<Message> ReceivedMessages { get; set; } 
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
