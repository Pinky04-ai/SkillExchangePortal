namespace SkillExchange.Blazor.Models
{
    public class FeedbackResponse
    {
        public int FeedbackId { get; set; }
        public int ContentId { get; set; }
        public string ContentTitle { get; set; } = "";
        public int UserId {  get; set; }
        public string UserName { get; set; } = "";
        public int Rating { get; set; }
        public string Comment { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
