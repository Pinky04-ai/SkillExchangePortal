using SkillExchange.BAL.Interface;
using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;
using SkillExchange.DAL.Interfaces;
namespace SkillExchange.BAL.Managers
{
    public class FeedbackManager : IFeedbackManager
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IContentRepository _contentRepository;
        public FeedbackManager(IFeedbackRepository feedbackRepository,IContentRepository contentRepository)
        {
            _feedbackRepository = feedbackRepository;
            _contentRepository = contentRepository;
        }
        public async Task<FeedbackDTO> AddAsync(CreateFeedbackDTO dto)
        {
            var existingFeedback = await _feedbackRepository.GetByUserAndContentAsync(dto.UserId, dto.ContentId);
            if (existingFeedback != null)
                throw new InvalidOperationException("User has already submitted feedback for this content.");
            var feedback = new Feedback
            {
                ContentId = dto.ContentId,
                UserId = dto.UserId,
                Comment = dto.Comment,
                Rating = dto.Rating,
                CreatedAt = DateTime.UtcNow
            };
            await _feedbackRepository.AddAsync(feedback);
            var insertedFeedback = await _feedbackRepository.GetByIdAsync(feedback.Id);
            return new FeedbackDTO
            {
                Id = insertedFeedback.Id,
                ContentId = insertedFeedback.ContentId,
                ContentTitle = insertedFeedback.Content?.Title ?? "Unknown",
                UserId = insertedFeedback.UserId,
                UserName = insertedFeedback.User?.FirstName ?? "Unknown",
                Comment = insertedFeedback.Comment,
                Rating = insertedFeedback.Rating,
                CreatedAt = insertedFeedback.CreatedAt
            };
        }
        public async Task<ContentDTO> GetContentByIdAsync(int contentId)
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
                ContentType = content.ContentType,
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
        public async Task<bool> DeleteAsync(int id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null) return false;
            await _feedbackRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<FeedbackDTO>> GetAllAsync()
        {
            var feedbacks = await _feedbackRepository.GetAllAsync();
            return feedbacks.Select(feedback => new FeedbackDTO
            {
                Id = feedback.Id,
                ContentId = feedback.ContentId,
                ContentTitle = feedback.ContentTitle ?? "Unknown",
                UserId = feedback.UserId,
                UserName = feedback.UserName ?? "Unknown",
                Comment = feedback.Comment,
                Rating = feedback.Rating,
                CreatedAt = feedback.CreatedAt
           });
        }
        public async Task<Feedback> GetByIdAsync(int id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null)
                return null;
            return new Feedback
            {
                Id = feedback.Id,
                ContentId = feedback.ContentId,
                ContentTitle = feedback.ContentTitle ?? "Unknown",
                UserId = feedback.UserId,
                UserName = feedback.UserName ?? "Unknown",
                Comment = feedback.Comment,
                Rating = feedback.Rating,
                CreatedAt = feedback.CreatedAt
            };
        }
        public async Task<FeedbackDTO?> UpdateAsync(int id, CreateFeedbackDTO dto)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null) return null;
            feedback.Comment = dto.Comment;
            feedback.Rating = dto.Rating;
            await _feedbackRepository.UpdateAsync(feedback);
            return new FeedbackDTO
            {
                Id = feedback.Id,
                ContentId = feedback.ContentId,
                ContentTitle = feedback.Content?.Title ?? string.Empty,
                UserId = feedback.UserId,
                UserName = feedback.User?.FirstName ?? "Unknown",
                Comment = feedback.Comment,
                Rating = feedback.Rating,
                CreatedAt = feedback.CreatedAt
            };   
        }
    }
}
