using Microsoft.AspNetCore.Mvc;
using SkillExchange.API.DTO.Feedback;
using SkillExchange.BAL.Interfaces;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FeedbacksController : ControllerBase
{
    private readonly IFeedbackManager _manager;

    public FeedbacksController(IFeedbackManager manager)
    {
        _manager = manager;
    }

    // POST: api/feedbacks
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFeedbackDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _manager.AddAsync(dto.UserId, dto);

        if (created == null)
        {
            return BadRequest("User not verified or does not exist.");
        }

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }


    [HttpGet("content/{contentId:int}")]
    public async Task<IActionResult> GetByContent(int contentId)
    {
        var items = await _manager.GetAllByContentAsync(contentId);
        return Ok(items);
    }


    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var items = await _manager.GetAllByUserAsync(userId);
        return Ok(items);
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dto = await _manager.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    // PUT: api/feedbacks/123
    //[HttpPut("{id:int}")]
    //public async Task<IActionResult> Update(int id, [FromBody] UpdateFeedbackDTO dto)
    //{
    //    if (!ModelState.IsValid) return BadRequest(ModelState);

    //    var ok = await _manager.UpdateAsync(id, dto);
    //    if (!ok) return NotFound();
    //    return NoContent();
    //}

    // DELETE: api/feedbacks/123
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _manager.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
