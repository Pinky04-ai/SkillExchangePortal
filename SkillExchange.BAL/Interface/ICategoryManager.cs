using SkillExchange.Core.DTO;
namespace SkillExchange.BAL.Interface
{
    public interface ICategoryManager
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<CategoryDTO> AddAsync(CreateCategoryDTO dto);
        Task<CategoryDTO?> UpdateAsync(int id, CreateCategoryDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
