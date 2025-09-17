using Microsoft.AspNetCore.Identity;
using SkillExchange.API.DTO.Content;
using SkillExchange.BAL.Interfaces;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkillExchange.DAL.Enums.Enum;

namespace SkillExchange.BAL.Manager
{
    public class ContentManager : IContentManager
    {
        private readonly IContentItem _contentRepo;
        private readonly IAppUser _userRepo;
        private readonly ICategory _categoryRepo;

       
        public ContentManager(IContentItem contentRepo, IAppUser userRepo, ICategory categoryRepo)
        {
            _contentRepo = contentRepo;
            _userRepo = userRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task ApproveContentAsync(int contentId, bool approve)
        {
            var c = await _contentRepo.GetByIdAsync(contentId);
            if (c == null) throw new Exception("Content not found");
            c.Status = approve ? ContentStatus.Approved : ContentStatus.Rejected;
            await _contentRepo.UpdateAsync(c);
        }


        public async Task<ContentDetailDTO?> GetContentDetailAsync(int id)
        {
            var content = await _contentRepo.GetByIdAsync(id);
            if (content == null) return null;

            return new ContentDetailDTO
            {
                Id = content.Id,
                Title = content.Title ?? string.Empty,
                Description = content.Description ?? string.Empty,
                CategoryName = content.Category?.Name ?? string.Empty,
                FileUrl = content.FileUrl ?? string.Empty,
                UploaderId = content.UserId,
                UploaderName = content.User?.FullName ?? string.Empty,
                CreatedAt = content.CreatedAt
            };
        }
        

       public async Task<IEnumerable<ContentListDTO>> GetPendingContentsAsync()
        {
            var contents = await _contentRepo.GetPendingApprovalAsync();

            return contents.Select(c => new ContentListDTO
            {
                Id = c.Id,
                Title = c.Title ?? string.Empty,
                Category = c.Category?.Name ?? string.Empty,
                Rating = c.Stars ?? 0,      
                ReviewsCount = c.Feedbacks?.Count ?? 0,
                FileUrl = c.FileUrl ?? string.Empty
            }).ToList();
        }

        public async Task<IEnumerable<ContentListDTO>> SearchContentsAsyncManager(string? title, int? categoryId, int? minStars = null, int? page = null, int? pageSize = null)
        {
            var contents = await _contentRepo.SearchContentsAsync(title, categoryId, minStars, page, pageSize, onlyApproved: true);

            return contents.Select(c => new ContentListDTO
            {
                Id = c.Id,
                Title = c.Title ?? string.Empty,
                Category = c.Category?.Name ?? string.Empty,
                Rating = c.Stars ?? (c.Feedbacks?.Any() == true ? c.Feedbacks.Average(f => (double)f.Rating) : 0),
                ReviewsCount = c.Feedbacks?.Count ?? 0,
                FileUrl = c.FileUrl ?? string.Empty
            }).ToList();
        }

        public async Task<ContentDTO> UploadContentAsync(CreateContentDTO dto, int userId, CreateContentDTO createContentDTO)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            var category = await _categoryRepo.GetByIdAsync(dto.CategoryId);

            if (user == null || category == null)
                throw new Exception("Invalid User or category");

            var content = new ContentItem
            {
                Title = createContentDTO.Title,
                Description = dto.Description,
                FileUrl = dto.FileUrl,
                UserId = userId,
                CategoryId = dto.CategoryId,
                Status = DAL.Enums.Enum.ContentStatus.PendingApproval,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _contentRepo.AddAsync(content);

            return new ContentDTO
            {
                Id = content.Id,
                Title = content.Title ?? string.Empty,
                Description = content.Description,
                FileUrl = content.FileUrl ?? string.Empty,
                Status = content.Status.ToString(),
                CategoryId = category.Id,
                CategoryName = category.Name,
                UserId = user.Id,
                UserFullName = user.FullName,
                CreatedAt = content.CreatedAt,
                UpdatedAt = content.UpdatedAt
            };
        }


    }
}
