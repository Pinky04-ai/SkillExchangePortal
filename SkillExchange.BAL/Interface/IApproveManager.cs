using SkillExchange.Core.DTO;
namespace SkillExchange.BAL.Interface
{
    public interface IApproveManager
    {
        Task<List<ApproveDTO>> GetPendingUsers();
        Task<bool> ApproveUser(int id);
        Task<bool> RejectUser(int id);
    }
}
