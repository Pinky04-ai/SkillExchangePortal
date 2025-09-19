using Microsoft.AspNetCore.Mvc;
using SkillExchange.API.DTO.Message;
using SkillExchange.BAL.Interfaces;

namespace SkillExchange.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageManager _messageManager;

        public MessageController(IMessageManager messageManager)
        {
            _messageManager = messageManager;
        }

        [HttpPost("send/{userId}")]
        public async Task<ActionResult<MessageDTO>> SendMessage(int userId, [FromBody] SendMessageDTO dto)
        {
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

        [HttpGet("inbox/{userId}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetInbox(int userId)
        {
            var messages = await _messageManager.GetInboxAsync(userId);
            return Ok(messages);
        }

        [HttpGet("sent/{userId}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetSentMessages(int userId)
        {
            var messages = await _messageManager.GetSentAsync(userId);
            return Ok(messages);
        }

        [HttpGet("conversation/{userId}/{otherUserId}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetConversation(int userId, int otherUserId)
        {
            var conversation = await _messageManager.GetConversationAsync(userId, otherUserId);
            return Ok(conversation);
        }

        [HttpPost("markasread/{userId}/{messageId}")]
        public async Task<ActionResult> MarkAsRead(int userId, int messageId)
        {
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

        [HttpDelete("{userId}/{messageId}")]
        public async Task<ActionResult> DeleteMessage(int userId, int messageId)
        {
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
