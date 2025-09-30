using SkillExchange.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.Core.DTO
{
   public class ContentWithRatingDTO
    {
        public Content Content { get; set; } = null!;
        public double AverageRating { get; set; }
        public int RatingCount { get; set; }
    }
}
