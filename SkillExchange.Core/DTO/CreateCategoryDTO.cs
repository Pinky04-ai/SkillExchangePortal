using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.Core.DTO
{
    public class CreateCategoryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
