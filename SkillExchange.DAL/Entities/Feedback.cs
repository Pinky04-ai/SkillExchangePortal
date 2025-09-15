using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int ContentId { get; set; }
        public AppUser User { get; set; }
        public ContentItem Content { get; set; }

    }
}
