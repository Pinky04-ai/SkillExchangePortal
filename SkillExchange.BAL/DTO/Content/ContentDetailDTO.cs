namespace SkillExchange.API.DTO.Content
{
    public class ContentDetailDTO
    {
        public int Id {  get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string FileUrl { get; set; }
        public int UploaderId { get; set; }
        public string UploaderName {  get; set; }
        public object CategoryName { get; set; }
        public object CreatedAt { get; set; }
        public object CreatedBy { get; set; }
    }
}
