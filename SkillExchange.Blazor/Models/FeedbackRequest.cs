namespace SkillExchange.Blazor.Models
{
    public class FeedbackRequest
    {
        public int ContentId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
        public int UserId {  get; set; }
    }
}
