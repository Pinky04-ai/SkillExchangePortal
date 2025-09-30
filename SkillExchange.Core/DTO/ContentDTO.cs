using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkillExchange.Core.Enum.Enum;

namespace SkillExchange.Core.DTO
{
    public class ContentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string ContentType { get; set; } = null!;
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FileUrl { get; set; } = null!;
        public ContentStatus Status { get; set; }
        public string? FileName { get; set; }
        public int UploadedById { get; set; }
        public int? ApprovedById { get; set; }
        public List<FeedbackDTO> Feedbacks { get; set; } = new();
    }
}
