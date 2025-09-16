namespace SkillExchange.API.DTO.Feedback
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public int FeedbackRatings {  get; set; }
        public string? Comment {  get; set; }
        public string UserName {  get; set; }

    }
}
