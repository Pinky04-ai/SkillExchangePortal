using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillExchange.BAL.Interface;
using SkillExchange.BAL.Managers;
using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;

namespace SkillExchange.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryManager.GetAllAsync();
            return Ok(categories);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await _categoryManager.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDTO>> Add([FromBody] CreateCategoryDTO dto)
        {
            var category = await _categoryManager.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDTO>> Update(int id, [FromBody] CreateCategoryDTO dto)
        {
            var category = await _categoryManager.UpdateAsync(id, dto);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryManager.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

    }
}
