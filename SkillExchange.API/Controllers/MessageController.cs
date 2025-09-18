using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillExchange.API.DTO.Message;
using SkillExchange.BAL.Interfaces;
using System.Security.Claims;
namespace SkillExchange.API.Controllers
{
    
    public class MessageController : Controller
    {
        private readonly IMessageManager _messageManager;

        public MessageController(IMessageManager messageManager)
        {
            _messageManager = messageManager;
        }
        [HttpPost("send")]
        public async Task<ActionResult<MessageDTO>> SendMessage([FromBody] SendMessageDTO dto)
        {
            // Assume we get current logged-in userId (for example from JWT claims)
            var userId = int.Parse(User.FindFirst("Id")?.Value ?? "0");

            if (userId == 0) return Unauthorized("User not logged in.");

            try
            {
                var message = await _messageManager.SendMessageAsync(userId, dto);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("inbox")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetInbox()
        {
            var userId = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            if (userId == 0) return Unauthorized("User not logged in.");

            var messages = await _messageManager.GetInboxAsync(userId);
            return Ok(messages);
        }

        [HttpGet("sent")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetSentMessages()
        {
            var userId = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            if (userId == 0) return Unauthorized("User not logged in.");

            var messages = await _messageManager.GetSentAsync(userId);
            return Ok(messages);
        }
        [HttpGet("conversation/{otherUserId}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetConversation(int otherUserId)
        {
            var userId = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            if (userId == 0) return Unauthorized("User not logged in.");

            var conversation = await _messageManager.GetConversationAsync(userId, otherUserId);
            return Ok(conversation);
        }
        [HttpPost("markasread/{messageId}")]
        public async Task<ActionResult> MarkAsRead(int messageId)
        {
            var userId = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            if (userId == 0) return Unauthorized("User not logged in.");

            try
            {
                await _messageManager.MarkAsReadAsync(userId, messageId);
                return Ok("Message marked as read.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{messageId}")]
        public async Task<ActionResult> DeleteMessage(int messageId)
        {
            var userId = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            if (userId == 0) return Unauthorized("User not logged in.");

            try
            {
                await _messageManager.DeleteAsync(userId, messageId);
                return Ok("Message deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
