using SkillExchange.API.DTO.Feedback;
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
    public class FeedbackManager : IFeedbackManager
    {
        private readonly IFeedback _feedbackRepo;
        private readonly IAppUser _userRepo;

        public FeedbackManager(IFeedback feedbackRepo, IAppUser userRepo)
        {
            _feedbackRepo = feedbackRepo;
            _userRepo = userRepo;
        }

        public async Task<FeedbackDTO> AddAsync(int userId, CreateFeedbackDTO dto)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null || user.Status != UserStatus.Verified)
                throw new InvalidOperationException("Only verified users can submit feedback.");

            var feedback = new Feedback
            {
                ContentId = dto.ContentId,
                Rating = (FeedbackRating)dto.FeedbackRating,
                Comment = dto.Comment,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _feedbackRepo.AddAsync(feedback);

            return new FeedbackDTO
            {
                Id = feedback.Id,
                Rating = (int)feedback.Rating,
                Comment = feedback.Comment,
                UserName = user.FullName
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var feedback = await _feedbackRepo.GetByIdAsync(id);
            if (feedback == null) return false;

            await _feedbackRepo.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<FeedbackDTO>> GetAllByContentAsync(int contentId)
        {
            var feedbacks = await _feedbackRepo.GetAllByContentAsync(contentId);

            return feedbacks.Select(f => new FeedbackDTO
            {
                Id = f.Id,
                Rating = (int)f.Rating,
                Comment = f.Comment,
                UserName = f.User?.FullName ?? "Unknown"
            });
        }

        public async Task<IEnumerable<FeedbackDTO>> GetAllByUserAsync(int userId)
        {
            var feedbacks = await _feedbackRepo.GetAllByUserAsync(userId);

            return feedbacks.Select(f => new FeedbackDTO
            {
                Id = f.Id,
                Rating = (int)f.Rating,
                Comment = f.Comment,
                UserName = f.User?.FullName ?? "Unknown"
            });
        }

        public async Task<FeedbackDTO?> GetByIdAsync(int id)
        {
            var feedback = await _feedbackRepo.GetByIdAsync(id);
            if (feedback == null) return null;

            return new FeedbackDTO
            {
                Id = feedback.Id,
                Rating = (int)feedback.Rating,
                Comment = feedback.Comment,
                UserName = feedback.User?.FullName ?? "Unknown"
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateFeedbackDTO dto)
        {
            var feedback = await _feedbackRepo.GetByIdAsync(id);
            if (feedback == null) return false;

            feedback.Comment = dto.Comment;
            feedback.Rating = (FeedbackRating)dto.FeedbackRating;

            await _feedbackRepo.UpdateAsync(feedback);
            return true;
        }
    }
}
