using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillExchange.BAL.Interface;
using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;
namespace SkillExchange.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManager _feedbackManager;
        public FeedbackController(IFeedbackManager feedbackManager)
        {
            _feedbackManager = feedbackManager;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetAll()
        {
            var feedbacks = await _feedbackManager.GetAllAsync();
            return Ok(feedbacks);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Feedback>> GetById(int id)
        {
            var feedback = await _feedbackManager.GetByIdAsync(id);
            if (feedback == null) return NotFound();
            return Ok(feedback);
        }
        [HttpPost]
        public async Task<ActionResult<FeedbackDTO>> Add([FromBody] CreateFeedbackDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var feedback = await _feedbackManager.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = feedback.Id }, feedback);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<FeedbackDTO>> Update(int id, [FromBody] CreateFeedbackDTO dto)
        {
            var updated = await _feedbackManager.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _feedbackManager.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
