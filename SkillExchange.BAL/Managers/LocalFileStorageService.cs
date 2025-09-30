using Microsoft.AspNetCore.Http;
using SkillExchange.BAL.Interface;

namespace SkillExchange.BAL.Managers
{
    public class LocalFileStorageService : IFileStorageService
    {
       
        private readonly string _uploadPath = Path.Combine("wwwroot", "uploads");
        public async Task<string> UploadAsync(IFormFile file)
        {
            Directory.CreateDirectory(_uploadPath); 
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_uploadPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return $"/uploads/{fileName}";
        }
    }
}
