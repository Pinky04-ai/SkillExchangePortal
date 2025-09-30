using Microsoft.AspNetCore.Mvc;
using SkillExchange.BAL.Interface;
using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;

namespace SkillExchange.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminManager _adminManager;

        public AdminController(IAdminManager adminManager)
        {
            _adminManager = adminManager;
        }
        [HttpGet("pending-content")]
        public async Task<ActionResult<List<ContentDTO>>> GetPendingContent()
        {
            var contents = await _adminManager.GetPendingContentAsync();
            return Ok(contents);
        }
        [HttpPost("approve-content/{id}")]
        public async Task<IActionResult> ApproveContent(int id)
        {
            var success = await _adminManager.ApproveContentAsync(id);
            if (!success) return NotFound();
            return Ok(new { message = "Content approved successfully" });
        }
        [HttpPost("reject-content/{id}")]
        public async Task<IActionResult> RejectContent(int id)
        {
            var success = await _adminManager.RejectContentAsync(id);
            if (!success) return NotFound();
            return Ok(new { message = "Content rejected successfully" });
        }
        [HttpPost("upload-content")]
        public async Task<IActionResult> UploadContent([FromBody] Content content)
        {
            await _adminManager.AddContentAsync(content);
            return Ok(new { message = "Content uploaded successfully. Pending admin approval." });
        }
    }
}
