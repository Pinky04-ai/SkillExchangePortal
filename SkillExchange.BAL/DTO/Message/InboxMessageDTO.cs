using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.BAL.DTO.Message
{
    public class InboxMessageDTO
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public string FromUserEmail { get; set; }
        public string ToUserEmail { get; set; }
    }

}
