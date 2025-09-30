using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.Core.DTO
{
    public class CreateContentDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public int CategoryId {  get; set; }
        public int UploadedById { get; set; }
        [Required]
        public string FileName { get; set; }
        public  IFormFile? File { get; set; }
    }
}
