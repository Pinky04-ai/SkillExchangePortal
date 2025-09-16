using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.DAL.Enums
{
    public class Enum
    {
        public enum UserStatus
        {
            UnderVerification = 0,
            Verified = 1,
            Suspended = 2
        }
        public enum ContentStatus
        {
            PendingApproval = 0,
            Approved = 1,
            Rejected = 2
        }
        public enum UserRoleType
        {
            Admin = 1,
            User = 2
        }
        public enum FeedbackRating
        {
            VeryBad = 1,
            Bad = 2,
            Average = 3,
            Good = 4,
            Excellent = 5
        }
        public enum MessageStatus
        {
            Sent = 0,
            Delivered = 1,
            Read = 2
        }
    }
}
