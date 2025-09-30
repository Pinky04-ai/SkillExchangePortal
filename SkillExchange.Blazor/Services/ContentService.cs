//using SkillExchange.Blazor.Models;
//using System.Net.Http.Json;
//using static System.Net.WebRequestMethods;

//namespace SkillExchange.Blazor.Services
//{
//    public class ContentService
//    {
//        private readonly HttpClient _httpClient;
//        public ContentService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<List<Content>> GetAllApprovedContentAsync()
//        {
//            try
//            {

//                var response = await _httpClient.GetAsync("api/content");

//                if (!response.IsSuccessStatusCode)
//                {
//                    return new List<Content>();
//                }

//                var data = await response.Content.ReadFromJsonAsync<List<Content>>();

//                return data ?? new List<Content>();
//            }
//            catch (HttpRequestException)
//            {

//                return new List<Content>();
//            }
//            catch
//            {
//                return new List<Content>();
//            }
//        }
//        public async Task<Content> GetContentByIdAsync(int id)
//        {
//            try
//            {
//                var response = await _httpClient.GetAsync($"api/content/{id}");
//                if (!response.IsSuccessStatusCode) return null;

//                var content = await response.Content.ReadFromJsonAsync<Content>();
//                return content;
//            }
//            catch
//            {
//                return null;
//            }
//        }

//        // Submit feedback
//        public async Task SubmitFeedbackAsync(Feedback feedback)
//        {
//            try
//            {
//                await _httpClient.PostAsJsonAsync("api/content/feedback", feedback);
//            }
//            catch
//            {
//                // Handle failure silently
//            }
//        }
//        public async Task<List<Content>> GetMyCoursesAsync()
//        {
//            try
//            {
//                return await _httpClient.GetFromJsonAsync<List<Content>>("api/content/mycourses");
//            }
//            catch { return new List<Content>(); }
//        }
//        public async Task<List<Content>> GetAllContentForAdminAsync()
//        {
//            try
//            {
//                return await _httpClient.GetFromJsonAsync<List<Content>>("api/admin/content");
//            }
//            catch { return new List<Content>(); }
//        }
//        public async Task SubmitContentAsync(Content newContent)
//        {
//            var response = await _httpClient.PostAsJsonAsync("api/content", newContent);

//            if (!response.IsSuccessStatusCode)
//            {
//                throw new Exception("Failed to submit content: " + response.ReasonPhrase);
//            }
//        }
//    }
//}
using Microsoft.AspNetCore.Components.Forms;
using SkillExchange.Blazor.Models;
using SkillExchange.Core.DTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SkillExchange.Blazor.Services
{
    public class ContentService
    {
        private readonly HttpClient _http;
        public ContentService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ContentDTO> CreateAsync(CreateContentDTO dto, IBrowserFile selectedFile)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(dto.Title ?? ""), "Title");
            content.Add(new StringContent(dto.Description ?? ""), "Description");
            content.Add(new StringContent(dto.CategoryId.ToString()), "CategoryId");
            content.Add(new StringContent(dto.ContentType ?? ""), "ContentType");
            content.Add(new StringContent(dto.UploadedById.ToString()), "UploadedById");
            content.Add(new StringContent(selectedFile.Name ?? ""), "FileName");

            var streamContent = new StreamContent(selectedFile.OpenReadStream(200 * 1024 * 1024));
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(selectedFile.ContentType);
            content.Add(streamContent, "File", selectedFile.Name);

            var response = await _http.PostAsync("api/content/create", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorText = await response.Content.ReadAsStringAsync();
                
                throw new Exception($"Upload failed: {response.StatusCode}. Server response: {errorText}");
            }

            return await response.Content.ReadFromJsonAsync<ContentDTO>();
        }
        public async Task<bool> UpdateStatusAsync(UpdateStatusDTO dto)
        {
            var resp = await _http.PostAsJsonAsync("api/content/update-status", dto);
            return resp.IsSuccessStatusCode;
        }
        public async Task<List<ContentDTO>> GetApprovedContentsAsync()
        {
            return await _http.GetFromJsonAsync<List<ContentDTO>>("api/content/approved") ?? new();
        }
        public async Task<ContentDTO> GetContentByIdAsync(int contentId)
        {
     
            var contentDto = await _http.GetFromJsonAsync<ContentDTO>($"api/content/{contentId}");
            return contentDto;
        }
        public async Task<bool> SubmitFeedbackAsync(FeedbackRequestDTO feedbackDto)
        {
            var response = await _http.PostAsJsonAsync("api/content/{contentId}/feedback", feedbackDto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> SubmitContentAsync(Content content)
        {
            var dto = new Content
            {
                Title = content.Title,
                Description = content.Description,
                CategoryId = content.CategoryId,
                FileType = content.FileType,
                FileUrl = content.FileUrl
            };

            var response = await _http.PostAsJsonAsync("api/content/submit", dto);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<Content>> GetAllContentsAsync()
        {
            var result = await _http.GetFromJsonAsync<List<Content>>("api/content");
            return result ?? new List<Content>();
        }
        public async Task<ContentDTO?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ContentDTO>($"api/content/{id}");
        }
        public async Task<IEnumerable<ContentDTO>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<ContentDTO>>("api/content/all") ?? new List<ContentDTO>();
        }
        public async Task<IEnumerable<ContentDTO>> GetByCategoryAsync(int categoryId)
        {
            return await _http.GetFromJsonAsync<IEnumerable<ContentDTO>>($"api/content/category/{categoryId}") ?? new List<ContentDTO>();
        }
        public async Task<IEnumerable<ContentDTO>> SearchAsync(string? q, int? categoryId)
        {
            var query = $"api/content/search?q={q}&categoryId={categoryId}";
            return await _http.GetFromJsonAsync<IEnumerable<ContentDTO>>(query) ?? new List<ContentDTO>();
        }
        public async Task<IEnumerable<ContentDTO>> GetPendingAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<ContentDTO>>("api/content/pending") ?? new List<ContentDTO>();
        }
        public async Task<List<ContentDTO>> GetPendingContentsAsync()
        {
            return await _http.GetFromJsonAsync<List<ContentDTO>>("api/admin/pending-content");
        }
        public async Task ApproveAsync(int contentId, int approverId)
        {
            var response = await _http.PostAsync($"api/content/{contentId}/approve?approverId={approverId}", null);
            response.EnsureSuccessStatusCode();
        }
        public async Task<List<FeedbackDTO>> GetAllFeedbacksForContentAsync(int contentId)
        {
            var response = await _http.GetFromJsonAsync<List<FeedbackDTO>>($"api/content/{contentId}/feedbacks");
            return response ?? new List<FeedbackDTO>();
        }
        public async Task RejectAsync(int contentId, int approverId)
        {
            var response = await _http.PostAsync($"api/content/{contentId}/reject?approverId={approverId}", null);
            response.EnsureSuccessStatusCode();
        }
    }
}

