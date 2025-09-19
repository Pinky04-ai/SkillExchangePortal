namespace SkillExchange.API.DTO.Feedback
{
    public class CreateFeedbackDTO
    {
        public int UserId { get; set; }
        public int ContentId {  get; set; }
        public int FeedbackRating {  get; set; }
        public string? Comment {  get; set; }

    }
}
