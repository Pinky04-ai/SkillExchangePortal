namespace SkillExchange.API.DTO.Content
{
    public class CreateContentDTO
    {
        public string Title { get; set; } = default!;
        public string Description {  get; set; }
        public int CategoryId {  get; set; }
        public string FileUrl {  get; set; }

    }
}
