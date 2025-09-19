using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.BAL.DTO.Feedback
{
    public class FeedbackWithUserDto
    {
        public int Id { get; set; }          // Feedback Id
        public int ContentId { get; set; }
        public int UserId { get; set; }      // Feedback UserId
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public int User_Id { get; set; }     // User.Id
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
