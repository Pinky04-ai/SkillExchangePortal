using Microsoft.AspNetCore.Components.Authorization;
using SkillExchange.Blazor.Models;
using SkillExchange.Core.DTO;
using SkillExchange.Infrastructure.Data;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using static SkillExchange.Core.Enum.Enum;
using User = SkillExchange.Blazor.Models.User;
namespace SkillExchange.Blazor.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        public User? CurrentUsers { get; private set; }
        private LoginResponse? _currentUser;
        private string? _jwtToken;
        public AuthService(HttpClient http)
        {
            _http = http;
            
        }
        public bool isLoggedin { get; set; } = false;
        public event Action? OnAuthStateChanged;
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Auth/login", request);

                if (response.IsSuccessStatusCode)
                {
                    _currentUser = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    if (_currentUser != null)
                    {
                        _jwtToken = _currentUser.Token;
                        isLoggedin = true;
                    }
                    return _currentUser;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Login failed: {error}");
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during login: {ex.Message}");
                return null;
            }
        }
        public void SetCurrentUser(User user) => CurrentUsers = user;
        public async Task<User> GetCurrentUserAsync()
        {
            if (CurrentUser != null)
                return CurrentUsers;

            throw new Exception("No authenticated user set. Please log in first.");
        }
        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            isLoggedin = true;
            var response = await _http.PostAsJsonAsync("api/Auth/register", request);
            return response.IsSuccessStatusCode;
        }
        public void Logout()
        {
            _currentUser = null;
        }
        public bool IsLoggedIn => _currentUser != null;
        public LoginResponse? CurrentUser => _currentUser;
        private readonly List<User> _users = new()
        {
        new User { Id = 1, Email = "admin@skillexchange.com", Password = "Admin@123", Role = "Admin" , VerifiedAt=DateTime.UtcNow},
       
        };
        public Task<bool> AdminLogin(string email, string password)
        {
            isLoggedin= true;
            var admin = _users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password &&
                u.Role == "Admin");

            return Task.FromResult(admin != null);
        }
        public async Task<List<UserDTO>> GetPendingUsers()
        {
            return await _http.GetFromJsonAsync<List<UserDTO>>("api/approve/pendingcontent");
             
        }
        public async Task ApproveUser(int id)
        {
            await _http.PostAsync("api/approve/approve/{id}", null);

        }
        public async Task RejectUser(int id)
        {
            await _http.PostAsync("api/approve/reject/{id}", null);
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _http.GetFromJsonAsync<List<User>>("api/Auth")
                   ?? new List<User>();
        }
    }
}

