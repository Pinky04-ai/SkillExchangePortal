using SkillExchange.Blazor.Models;
using System.Net.Http.Json;

namespace SkillExchange.Blazor.Services
{
    public class AdminService
    {
        private readonly HttpClient _http;
        public AdminService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<Content>> GetPendingContentAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<Content>>("api/admin/pendingcontent")
                       ?? new List<Content>();
            }
            catch
            {
                return new List<Content>();
            }
        }
        public async Task<bool> ApproveContentAsync(int contentId)
        {
            try
            {
                var response = await _http.PostAsync($"api/admin/approve/{contentId}", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> RejectContentAsync(int contentId)
        {
            try
            {
                var response = await _http.PostAsync($"api/admin/reject/{contentId}", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
