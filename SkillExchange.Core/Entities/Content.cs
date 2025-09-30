using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkillExchange.Core.Enum.Enum;

namespace SkillExchange.Core.Entities
{
    public class Content
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string ContentType { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public string? FileName { get; set; }
        public int CategoryId { get; set; }
        public int UploadedById { get; set; }
        public int? ApprovedById { get; set; }
        public ContentStatus Status { get; set; } = ContentStatus.PendingApproval;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRejected { get; set; }
        public bool IsApproved { get; set; }
        public Category? Category { get; set; }
        public User? UploadedBy { get; set; }
        public User? ApprovedBy { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
