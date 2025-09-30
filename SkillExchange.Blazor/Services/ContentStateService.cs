using SkillExchange.Core.DTO;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace SkillExchange.Blazor.Services
{
    public class ContentStateService
    {
        public event Action? OnContentApproved;
        public void NotifyContentApproved() => OnContentApproved?.Invoke();
       
    }
}
