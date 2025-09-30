using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillExchange.BAL.Interface;
using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;
using static SkillExchange.Core.Enum.Enum;
using Content = SkillExchange.Core.Entities.Content;
namespace SkillExchange.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly IContentManager _contentManager;
        private readonly IWebHostEnvironment _env;
        public ContentController(IContentManager contentManager, IWebHostEnvironment env)
        {
            _contentManager = contentManager;
            _env = env;
        }
        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDTO dto)
        {
            if (dto == null) return BadRequest("Invalid payload.");
            var ok = await _contentManager.UpdateContentStatusAsync(dto);
            if (!ok) return BadRequest("Could not update status. Check inputs.");
            return Ok(new { message = "Status updated" });
        }
        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetContentById(int id)
        {
            if (id <= 0) return BadRequest("Invalid content id.");
            var contentDto = await _contentManager.GetContentByIdAsync(id); 
            if (contentDto == null) return NotFound();
            return Ok(contentDto);
        }
        [HttpGet("{contentId}/feedbacks")]
        public async Task<IActionResult> GetFeedbacksForContent(int contentId)
        {
            var feedbacks = await _contentManager.GetAllFeedbacksForContentAsync(contentId); // Use BAL
            return Ok(feedbacks);
        }
        [HttpPost("submit-feedback")]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackRequestDTO feedbackDto)
        {
            if (feedbackDto == null) return BadRequest("Feedback cannot be null.");
            if (feedbackDto.ContentId <= 0 || feedbackDto.UserId <= 0 || feedbackDto.Rating < 1 || feedbackDto.Rating > 5)
                return BadRequest("Invalid feedback data.");
            var result = await _contentManager.SubmitFeedbackAsync(feedbackDto);
            if (result)
                return Ok(new { message = "Feedback submitted successfully" });
            return BadRequest("Failed to submit feedback.");
        }
        [HttpGet("approved")]
        public async Task<IActionResult> GetApproved()
        {
            var list = await _contentManager.GetApprovedContentsAsync();
            return Ok(list);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateContentDTO dto, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required.");
            var uploadsFolder = Path.Combine(_env.ContentRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            var uniqueFileName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            await using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(stream);
            }
            var content = new Content
            {
                Title = dto.Title,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                ContentType = dto.ContentType,
                UploadedById = dto.UploadedById,
                FileName = uniqueFileName,
                FileUrl = $"/uploads/{uniqueFileName}",
                Status = ContentStatus.PendingApproval,
                CreatedAt = DateTime.UtcNow
            };
            await _contentManager.SubmitContentAsync(content);
            return Ok(new ContentDTO
            {
                Id = content.Id,
                Title = content.Title,
                Description = content.Description,
                CategoryId = content.CategoryId,
                ContentType = content.ContentType,
                FileName = content.FileName,
                FileUrl = content.FileUrl,
                Status = content.Status,
                CreatedAt = content.CreatedAt
            });
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ContentDTO>> GetById(int id)
        {
            var content = await _contentManager.GetByIdAsync(id);
            if (content == null) return NotFound();
            return Ok(content);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentDTO>>> GetAll()
        {
            var contents = await _contentManager.GetAllAsync();
            return Ok(contents);
        }
        [HttpGet("category/{categoryId:int}")]
        public async Task<ActionResult<IEnumerable<ContentDTO>>> GetByCategory(int categoryId)
        {
            var contents = await _contentManager.GetByCategoryAsync(categoryId);
            return Ok(contents);
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ContentDTO>>> Search([FromQuery] string? q, [FromQuery] int? categoryId)
        {
            var contents = await _contentManager.SearchAsync(q, categoryId);
            return Ok(contents);
        }
        [HttpPost("approve/{id:int}/{approverId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id, int approverId)
        {
            await _contentManager.ApproveAsync(id, approverId);
            return Ok(new { message = "Content approved successfully." });
        }
        [HttpPost("reject/{id:int}/{approverId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int id, int approverId)
        {
            await _contentManager.RejectAsync(id, approverId);
            return Ok(new { message = "Content rejected successfully." });
        }
        [HttpGet("pending")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ContentDTO>>> GetPending()
        {
            var contents = await _contentManager.GetPendingAsync();
            return Ok(contents);
        }
    }
}
