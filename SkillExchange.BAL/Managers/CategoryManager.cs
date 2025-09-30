using SkillExchange.BAL.Interface;
using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;
using SkillExchange.DAL.Interfaces;

namespace SkillExchange.BAL.Managers
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryDTO> AddAsync(CreateCategoryDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _categoryRepository.AddAsync(category);
            return new CategoryDTO(category.Id, category.Name);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return false;

            await _categoryRepository.DeleteAsync(id);
            return true;
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDTO(c.Id, c.Name));
        }
        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryDTO(category.Id, category.Name);
        }
        public async Task<CategoryDTO?> UpdateAsync(int id, CreateCategoryDTO dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _categoryRepository.UpdateAsync(category);
            return new CategoryDTO(category.Id, category.Name);

        }
    }
}
