using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.Core.DTO
{
    public class FeedbackRequestDTO
    {
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public string UserName {  get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
