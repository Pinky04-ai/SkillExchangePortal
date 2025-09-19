namespace SkillExchange.API.DTO.Feedback
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public int Ratings {  get; set; }
        public string? Comment {  get; set; }
        public string UserName {  get; set; }
        public int Rating { get; set; }
        public object UserId { get; internal set; }
        public object ContentId { get; internal set; }
        public object CreatedAt { get; internal set; }
    }
}
