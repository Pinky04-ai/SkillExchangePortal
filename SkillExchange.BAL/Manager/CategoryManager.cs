using SkillExchange.BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.BAL.Manager
{
    public class CategoryManager : ICategoryManager
    {
        public CategoryDTO CreateCategory(CreateCategoryDTO dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            throw new NotImplementedException();
        }
        private readonly ICategoryRepository _repo;

        public CategoryManager(ICategory repo)
        {
            _repo = repo;
        }

        public CategoryDTO CreateCategory(CreateCategoryDTO dto)
        {
            var category = new Category { Name = dto.Name };
            _repo.Add(category);

            return new CategoryDTO { Id = category.Id, CateoryName = category.Name };
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _repo.GetAll()
                        .Select(c => new CategoryDTO
                        {
                            Id = c.Id,
                            CateoryName = c.Name
                        });
        }

    }
}
