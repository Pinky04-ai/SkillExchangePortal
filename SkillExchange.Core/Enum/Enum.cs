using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.Core.Enum
{
    public class Enum
    {
        public enum UserRole { User = 0, Admin = 1 }
        public enum UserStatus { UnderVerification = 0, Verified = 1, Rejected = 2 ,Pending=3}
        public enum ContentStatus { PendingApproval = 0, Approved = 1, Rejected = 2, Draft = 3 }
        public enum FileType { PDF,Image,Video}
    }
}
