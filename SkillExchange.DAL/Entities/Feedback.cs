using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkillExchange.DAL.Enums.Enum;

namespace SkillExchange.DAL.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        [MaxLength(1000)]
        public string? Comment { get; set; }
        [Required]
        [EnumDataType(typeof(FeedbackRating))]
        public FeedbackRating Rating { get; set; }
        public int UserId { get; set; }
        [Required]
        public int ContentId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public AppUser User { get; set; }
        public ContentItem Content { get; set; }

    }
}
