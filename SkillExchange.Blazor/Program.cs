using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http.Features;
using MudBlazor.Services;
using SkillExchange.Blazor.Services;


namespace SkillExchange.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<ContentService>();
            builder.Services.AddScoped<FeedbackService>();
            builder.Services.AddSingleton<ContentStateService>();
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44340/")
            });
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 200 * 1024 * 1024; 
            });

            builder.RootComponents.Add<App>("#app");
            builder.Services.AddMudServices();
            builder.Services.AddMudServices();
            await builder.Build().RunAsync();
            
        }
    }
}
