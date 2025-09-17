
namespace SkillExchange.API.DTO.Content
{
    public class ContentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string FileUrl {  get; set; }
        public bool isApproved { get; set; }
        public string CategoryName {  get; set; }
        public string UploadedBy {  get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; }
        public string? UserFullName { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
