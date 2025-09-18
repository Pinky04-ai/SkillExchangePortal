using Microsoft.Extensions.Logging;
using SkillExchange.API.DTO.Category;
using SkillExchange.BAL.Interfaces;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Interface;
using SkillExchange.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.BAL.Manager
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategory _categoryRepo;
        private readonly IContentItem _contentRepo;
        private readonly ILogger<CategoryManager> _logger;
        public CategoryManager(
            ICategory categoryRepo,
            IContentItem contentRepo,
            ILogger<CategoryManager> logger)
        {
            _categoryRepo = categoryRepo;
            _contentRepo = contentRepo;
            _logger = logger;
        }
        public async Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var name = dto.Name.Trim();
            var existing = await _categoryRepo.GetByNameAsync(name);
            if (existing != null)
                throw new InvalidOperationException($"Category with name '{name}' already exists.");
            var entity = new Category
            {
                Name = name
            };
            await _categoryRepo.AddAsync(entity);
            return new CategoryDTO { Id = entity.Id, CategoryName = entity.Name };
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }
            if (category.Contents != null && category.Contents.Count > 0)
            {
                throw new InvalidOperationException(
                    "Cannot delete a category that still has content items."
                );
            }
            await _categoryRepo.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var list = await _categoryRepo.GetAllAsync();
            var result = new List<CategoryDTO>();
            foreach (var category in list)
            {
                result.Add(new CategoryDTO
                {
                    Id = category.Id,
                    CategoryName = category.Name
                });
            }
            return result;
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }
            return new CategoryDTO
            {
                Id = category.Id,
                CategoryName = category.Name
            };
        }

        public async Task<CategoryDTO?> UpdateAsync(int id, CreateCategoryDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Category name cannot be empty.");
            }
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }
            var newName = dto.Name.Trim();

            if (!string.Equals(category.Name, newName, StringComparison.OrdinalIgnoreCase))
            {
                var duplicate = await _categoryRepo.GetByNameAsync(newName);
                if (duplicate != null && duplicate.Id != id)
                {
                    throw new InvalidOperationException($"Category '{newName}' already exists.");
                }
            }
            category.Name = newName;
            await _categoryRepo.UpdateAsync(category);

            return new CategoryDTO
            {
                Id = category.Id,
                CategoryName = category.Name
            };
        }
    }
}
