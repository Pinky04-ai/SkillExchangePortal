using SkillExchange.BAL.Interface;
using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;
using SkillExchange.DAL.Interfaces;

namespace SkillExchange.BAL.Managers
{
    public class AdminManager : IAdminManager
    {
        private readonly IAdminRepository _repository;
        public AdminManager(IAdminRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<ContentDTO>> GetPendingContentAsync()
        {
            var contents = await _repository.GetPendingContentAsync();
            return contents.Select(c => new ContentDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CategoryName = c.Category.Name,
                IsApproved = c.IsApproved,
                IsRejected = c.IsRejected,
              
            }).ToList();
        }
        public async Task<bool> ApproveContentAsync(int contentId)
        {
            var content = await _repository.GetContentByIdAsync(contentId);
            if (content == null) return false;

            content.IsApproved = true;
            content.IsRejected = false;
            await _repository.UpdateContentAsync(content);
            return true;
        }
        public async Task<bool> RejectContentAsync(int contentId)
        {
            var content = await _repository.GetContentByIdAsync(contentId);
            if (content == null) return false;

            content.IsRejected = true;
            content.IsApproved = false;
            await _repository.UpdateContentAsync(content);
            return true;
        }
        public async Task AddContentAsync(Content content)
        {
            content.IsApproved = false;
            content.IsRejected = false;
            await _repository.AddContentAsync(content);
        }
    }
}
