using System.ComponentModel.DataAnnotations;

namespace SkillExchange.API.DTO.Category
{
    public class CreateCategoryDTO
    {
        [Required, MaxLength(100)]
        public string Name {  get; set; }
    }
}
