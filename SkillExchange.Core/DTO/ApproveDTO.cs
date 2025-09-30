using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.Core.DTO
{
    public class ApproveDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";         
        public string Status { get; set; } = "";        
        public DateTime? VerifiedAt { get; set; }
    }
}
