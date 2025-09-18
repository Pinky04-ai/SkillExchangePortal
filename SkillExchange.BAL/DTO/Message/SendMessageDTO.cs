using System.ComponentModel.DataAnnotations;

namespace SkillExchange.API.DTO.Message
{
    public class SendMessageDTO
    {
        [Required]
        public int ToUserId {  get; set; }
        [Required, MaxLength(2000)]
        public string? Content {  get; set; }

    }
}
