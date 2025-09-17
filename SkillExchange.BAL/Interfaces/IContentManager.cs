using SkillExchange.API.DTO.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillExchange.BAL.Interfaces
{
    public interface IContentManager
    {
        Task<ContentDTO> UploadContentAsync(CreateContentDTO dto, int userId, CreateContentDTO createContentDTO);
        Task<IEnumerable<ContentListDTO>> SearchContentsAsyncManager(string? title, int? categoryId , int? minStars = null, int? page = null, int? pageSize = null);
        Task<ContentDetailDTO?> GetContentDetailAsync(int id);
        Task ApproveContentAsync(int contentId, bool approve);
        Task<IEnumerable<ContentListDTO>> GetPendingContentsAsync();
    }
}
