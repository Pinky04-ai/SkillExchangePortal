using Microsoft.AspNetCore.Http;

namespace SkillExchange.BAL.Interface
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
