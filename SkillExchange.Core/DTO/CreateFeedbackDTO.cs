using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.Core.DTO
{
    public class CreateFeedbackDTO
    {
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
    }
}
