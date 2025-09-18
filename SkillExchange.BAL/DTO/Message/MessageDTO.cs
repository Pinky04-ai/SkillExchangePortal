namespace SkillExchange.API.DTO.Message
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int? FromUserId {  get; set; }
        public string? FromUserName { get; set; }
        public int? ToUserId { get; set; }
        public string? ToUserName { get; set; }
        public string Content {  get; set; }
        public bool IsRead { get; set; }
        public DateTime SentAt {  get; set; }

    }
}
