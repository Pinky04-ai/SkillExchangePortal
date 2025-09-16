namespace SkillExchange.API.DTO.Content
{
    public class ContentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileUrl {  get; set; }
        public bool isApproved { get; set; }
        public string CategoryName {  get; set; }
        public string UploadedBy {  get; set; }

    }
}
