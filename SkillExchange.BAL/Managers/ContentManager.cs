using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SkillExchange.BAL.Interface;
using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;
using SkillExchange.DAL.Interfaces;
using static SkillExchange.Core.Enum.Enum;
namespace SkillExchange.BAL.Managers
{
    public class ContentManager : IContentManager
    {
        private readonly IContentRepository _contentRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IFileStorageService _fileService;
        public ContentManager(IContentRepository contentRepository, ICategoryRepository categoryRepository,IFileStorageService fileService,IFeedbackRepository feedbackRepository)
        {
            _contentRepository = contentRepository;
            _categoryRepository = categoryRepository;
            _fileService = fileService;
            _feedbackRepository = feedbackRepository;
        }
        public async Task<ContentDTO> CreateAsync(CreateContentDTO dto, IFormFile file, string webRootPath)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is required.");

            if (file.Length > 200 * 1024 * 1024)
                throw new ArgumentException("File too large (max 200 MB)");
            var uploads = Path.Combine(webRootPath, "uploads");
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);
            var fileKey = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
            var savePath = Path.Combine(uploads, fileKey);
            await using var fs = new FileStream(savePath, FileMode.CreateNew);
            await file.CopyToAsync(fs);
            var content = new Content
            {
                Title = dto.Title,
                Description = dto.Description,
                ContentType = string.IsNullOrEmpty(dto.ContentType)
                              ? GetContentType(file.FileName)
                              : dto.ContentType,
                FileUrl = $"/uploads/{fileKey}",
                FileName = fileKey,
                CategoryId = dto.CategoryId,
                UploadedById = dto.UploadedById,
                CreatedAt = System.DateTime.UtcNow,
                Status = ContentStatus.PendingApproval,
                IsApproved = false,
                IsRejected = false
            };
            await _contentRepository.AddAsync(content);
            return new ContentDTO
            {
                Id = content.Id,
                Title = content.Title,
                Description = content.Description,
                CategoryId = content.CategoryId,
                CategoryName = content.Category?.Name,
                ContentType = content.ContentType,
                IsApproved = content.IsApproved,
                IsRejected = content.IsRejected,
                CreatedAt = content.CreatedAt,
                FileUrl = content.FileUrl,
                FileName = content.FileName,
                Status = content.Status,
                UploadedById = content.UploadedById,
                ApprovedById = content.ApprovedById
            };
        }
        public async Task<bool> SubmitContentAsync(Content content)
        {
            return await _contentRepository.SubmitAsync(content);
        }
        public async Task<ContentDTO?> GetByIdAsync(int id)
        {
            var content = await _contentRepository.GetByIdAsync(id);
            if (content == null) return null;

            return new ContentDTO
            {
                Id = content.Id,
                Title = content.Title,
                Description = content.Description,
                CategoryId = content.CategoryId,
                CategoryName = content.Category?.Name,
                ContentType = content.ContentType,
                IsApproved = content.IsApproved,
                IsRejected = content.IsRejected,
                CreatedAt = content.CreatedAt,
                FileUrl = content.FileUrl,
                FileName = content.FileName,
                Status = content.Status,
                UploadedById = content.UploadedById,
                ApprovedById = content.ApprovedById
            };
        }
        public async Task<bool> SubmitFeedbackAsync(FeedbackRequestDTO feedbackDto)
        {
            var content = await _contentRepository.GetByIdAsync(feedbackDto.ContentId);
            if (content == null) return false;

            var feedback = new Feedback
            {
                ContentId = feedbackDto.ContentId,
                ContentTitle = content.Title,
                UserId = feedbackDto.UserId,
                UserName = "User", 
                Comment = feedbackDto.Comment,
                Rating = feedbackDto.Rating,
                CreatedAt = DateTime.UtcNow
            };
            var added = await _feedbackRepository.AddAsync(feedback);
            return added != null;
        }
        public async Task<IEnumerable<ContentDTO>> GetAllAsync()
        {
            var list = await _contentRepository.GetAllAsync();
            return list.Select(c => new ContentDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CategoryId = c.CategoryId,
                CategoryName = c.Category?.Name,
                ContentType = c.ContentType,
                IsApproved = c.IsApproved,
                IsRejected = c.IsRejected,
                CreatedAt = c.CreatedAt,
                FileUrl = c.FileUrl,
                FileName = c.FileName,
                Status = c.Status,
                UploadedById = c.UploadedById,
                ApprovedById = c.ApprovedById
            }).ToList();
        }
        public async Task<IEnumerable<ContentDTO>> GetByCategoryAsync(int categoryId)
        {
            var list = await _contentRepository.GetByCategoryAsync(categoryId);
            return list.Select(c => new ContentDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CategoryId = c.CategoryId,
                CategoryName = c.Category?.Name,
                ContentType = c.ContentType,
                IsApproved = c.IsApproved,
                IsRejected = c.IsRejected,
                CreatedAt = c.CreatedAt,
                FileUrl = c.FileUrl,
                FileName = c.FileName,
                Status = c.Status,
                UploadedById = c.UploadedById,
                ApprovedById = c.ApprovedById
            }).ToList();
        }
        public async Task<IEnumerable<ContentDTO>> SearchAsync(string? q, int? categoryId)
        {
            var list = await _contentRepository.SearchAsync(q, categoryId);
            return list.Select(c => new ContentDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CategoryId = c.CategoryId,
                CategoryName = c.Category?.Name,
                ContentType = c.ContentType,
                IsApproved = c.IsApproved,
                IsRejected = c.IsRejected,
                CreatedAt = c.CreatedAt,
                FileUrl = c.FileUrl,
                FileName = c.FileName,
                Status = c.Status,
                UploadedById = c.UploadedById,
                ApprovedById = c.ApprovedById
            }).ToList();
        }

        public async Task ApproveAsync(int contentId, int approverId)
        {
            await _contentRepository.ApproveAsync(contentId, approverId, true);
        }

        public async Task RejectAsync(int contentId, int approverId)
        {
            await _contentRepository.ApproveAsync(contentId, approverId, false);
        }
        public async Task<bool> UpdateStatusAsync(UpdateStatusDTO dto)
        {
            if (dto == null || dto.ContentId <= 0)
                return false;
            var content = await _contentRepository.GetByIdAsync(dto.ContentId);
            if (content == null) return false;
            content.Status = dto.Status;
            await _contentRepository.UpdateAsync(content);
            return true;
        }
        public async Task<List<FeedbackDTO>> GetAllFeedbacksForContentAsync(int contentId)
        {
            var feedbacks = await _feedbackRepository.GetByContentIdAsync(contentId);
            return feedbacks.Select(f => new FeedbackDTO
            {
                Id = f.Id,
                ContentId = f.ContentId,
                UserId = f.UserId,
                UserName = f.UserName,
                Comment = f.Comment,
                Rating = f.Rating,
                CreatedAt = f.CreatedAt
            }).ToList();
        }
        public async Task<ContentDTO?> GetContentByIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);
            if (content == null) return null;
            var feedbacks = await _feedbackRepository.GetFeedbacksByContentIdAsync(contentId);
            return new ContentDTO
            {
                Id = content.Id,
                Title = content.Title,
                Description = content.Description,
                FileUrl = content.FileUrl,
                ContentType = content.FileUrl,
                CategoryName = content.Category?.Name ?? "",
                Feedbacks = feedbacks.Select(f => new FeedbackDTO
                {
                    Comment = f.Comment,
                    Rating = f.Rating,
                    UserName = f.UserName,
                    CreatedAt = f.CreatedAt
                }).ToList()
            };
        }
        public async Task<bool> UpdateContentStatusAsync(UpdateStatusDTO dto)
        {
            if (dto == null || dto.ContentId <= 0)
                return false;
            var content = await _contentRepository.GetByIdAsync(dto.ContentId);
            if (content == null) return false;
            if (!Enum.IsDefined(typeof(ContentStatus), dto.Status))
                return false;
            content.Status = dto.Status;  
            await _contentRepository.UpdateAsync(content);
            return true;
        }
        public async Task<List<ContentDTO>> GetApprovedContentsAsync()
        {
            var ents = await _contentRepository.GetApprovedContentsAsync();
            return ents.Select(e => new ContentDTO
            {
                Id = e.Id,
                Title = e.Title,
                FileUrl = e.FileUrl,
                FileName = e.FileName,
                Status = e.Status,
                ContentType = e.ContentType,
                CategoryId = e.CategoryId
            }).ToList();
        }
        public async Task<IEnumerable<ContentDTO>> GetPendingAsync()
        {
            var list = await _contentRepository.GetPendingAsync();
            return list.Select(c => new ContentDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CategoryId = c.CategoryId,
                CategoryName = c.Category?.Name,
                ContentType = c.ContentType,
                IsApproved = c.IsApproved,
                IsRejected = c.IsRejected,
                CreatedAt = c.CreatedAt,
                FileUrl = c.FileUrl,
                FileName = c.FileName,
                Status = c.Status,
                UploadedById = c.UploadedById,
                ApprovedById = c.ApprovedById
            }).ToList();
        }
        private string GetContentType(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            if (new[] { ".mp4", ".mov", ".webm", ".mkv" }.Contains(ext)) return "video";
            if (new[] { ".jpg", ".jpeg", ".png", ".gif", ".svg", ".webp" }.Contains(ext)) return "image";
            if (new[] { ".pdf", ".ppt", ".pptx", ".doc", ".docx" }.Contains(ext)) return "document";
            return "file";
        }
    }
}
