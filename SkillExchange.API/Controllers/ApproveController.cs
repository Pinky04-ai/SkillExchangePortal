using Microsoft.AspNetCore.Mvc;
using SkillExchange.BAL.Interface;
using SkillExchange.Core.DTO;
namespace SkillExchange.API.Controllers
{
    [ApiController]
    [Route("api/approve")]
    public class ApproveController : Controller
    {
        private readonly IApproveManager _approve;
        public ApproveController(IApproveManager approve)
        {
            _approve = approve;
        }
        [HttpGet("pendingcontent")]
        public async Task<ActionResult<List<UserDTO>>> GetPendingUsers()
        {
            var users = await _approve.GetPendingUsers();
            return Ok(users);
        }
        [HttpPost("approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            var success = await _approve.ApproveUser(id);
            if (!success) return NotFound();
            return Ok();
        }
        [HttpPost("reject/{id}")]
        public async Task<IActionResult> Reject(int     id)
        {
            var success = await _approve.RejectUser(id);
            if (!success) return NotFound();
            return Ok();
        }
    }
}
