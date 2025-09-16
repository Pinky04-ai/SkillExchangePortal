namespace SkillExchange.API.DTO.Message
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string FromUser {  get; set; }
        public string ToUser { get; set; }
        public string Content {  get; set; }
        public DateTime SentAt {  get; set; }

    }
}
