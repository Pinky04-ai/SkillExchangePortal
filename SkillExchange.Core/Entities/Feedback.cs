using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.Core.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string ContentTitle {  get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }    = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Content? Content { get; set; }
        public User? User { get; set; }

    }
}
