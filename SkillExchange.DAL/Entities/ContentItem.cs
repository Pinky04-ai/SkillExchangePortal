using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Entities
{
    public enum ContentStatus { PendingApproval = 0, Approved = 1, Rejected = 2 }
    public class ContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; } 
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public AppUser User { get; set; }
        public Category Category { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
