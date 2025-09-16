using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillExchange.API.DTO;
using SkillExchange.DAL.Entities;
using SkillExchange.DAL.Repository;

namespace SkillExchange.BAL.Interface
{
    public interface ICategoryManager
    {
        CategoryDTO CreateCategory(CreateCategoryDTO dto);
        IEnumerable<CategoryDTO> GetCategories();
    }
}
