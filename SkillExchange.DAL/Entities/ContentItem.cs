using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Entities
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static SkillExchange.DAL.Enums.Enum;

    public class ContentItem
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        [Url(ErrorMessage = "File URL must be a valid URL.")]
        public string? FileUrl { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public ContentStatus Status { get; set; } = ContentStatus.PendingApproval;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public double? Stars {  get; set; }
        public AppUser? User { get; set; }
        public Category? Category { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }

}
