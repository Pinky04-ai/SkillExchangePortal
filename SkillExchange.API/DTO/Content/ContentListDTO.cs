namespace SkillExchange.API.DTO.Content
{
    public class ContentListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category {  get; set; }
        public double Rating {  get; set; }
        public int ReviewsCount {  get; set; }
        public string FileUrl {  get; set; }
    }
}
