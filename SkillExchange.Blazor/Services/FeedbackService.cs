using SkillExchange.Blazor.Models;
using SkillExchange.Core.DTO;
using System.Net.Http.Json;

namespace SkillExchange.Blazor.Services
{
    public class FeedbackService
    {
        private readonly HttpClient _http;

        public FeedbackService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<FeedbackResponse>> GetAllFeedbacksAsync()
        {
            var result = await _http.GetFromJsonAsync<List<FeedbackResponse>>("api/feedback");
            return result ?? new List<FeedbackResponse>();
        }
        public async Task<bool> SubmitFeedbackAsync(FeedbackRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/feedback", request);
            return response.IsSuccessStatusCode;
        }
        public async Task<FeedbackDTO?> GetFeedbackByIdAsync(int id)
        {
            var feedback = await _http.GetFromJsonAsync<FeedbackDTO>($"api/feedback/{id}");
            return feedback;
        }
    }
}
