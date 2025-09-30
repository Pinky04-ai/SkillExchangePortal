using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static SkillExchange.Core.Enum.Enum;

namespace SkillExchange.Core.DTO
{
    public class UpdateStatusDTO
    {
        public int ContentId { get; set; }
        public ContentStatus Status { get; set; }
    }
}
