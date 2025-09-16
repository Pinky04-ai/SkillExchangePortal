using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkillExchange.DAL.Enums.Enum;

namespace SkillExchange.DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public int FromUserId { get; set; }
        [Required]
        public int ToUserId { get; set; }
        [Required]
        [MaxLength(2000, ErrorMessage = "Message cannot exceed 2000 characters.")]
        public string Content { get; set; }
        [Required]
        public bool IsRead { get; set; } = false;
        public DateTime SentAt { get; set; } = DateTime.Now;
        [Required]
        public MessageStatus Status { get; set; } = MessageStatus.Sent;
        public AppUser? FromUser { get; set; }
        public AppUser? ToUser { get; set; }
    }
}
