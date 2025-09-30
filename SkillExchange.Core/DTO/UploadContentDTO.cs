using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.Core.DTO
{
    public class UploadContentDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public int UploaderId { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}
