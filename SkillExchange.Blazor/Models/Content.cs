namespace SkillExchange.Blazor.Models
{
    public class Content
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "images/images.jpg"; 
        public string FileUrl { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty; 
        public List<Feedback> Feedbacks { get; set; } = new();
        public int CategoryId {  get; set; }
        public double AverageRating => Feedbacks?.Any() == true ? Feedbacks.Average(f => f.Rating) : 0;
    }
}
