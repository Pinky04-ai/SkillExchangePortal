using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SkillExchange.BAL.Interfaces;
using SkillExchange.BAL.Manager;
using SkillExchange.DAL.Database;
using SkillExchange.DAL.Interface;
using SkillExchange.DAL.Repository;
namespace SkillExchange.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkillExchange API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token.\r\nExample: \"Bearer abc123xyz\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IMessageManager, MessageManager>();
            builder.Services.AddScoped<IMessage, MessageRepositary>();
            builder.Services.AddScoped<IFeedbackManager, FeedbackManager>();
            builder.Services.AddScoped<IFeedback, FeedbackRepositary>();
            builder.Services.AddScoped<IAppUser, AppUserRepositary>();
            builder.Services.AddScoped<IUserManager,UserManager>(); 
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IRole, RoleRepositary>();
            builder.Services.AddScoped<IRoleManager, RoleManager>();

            builder.Services.AddControllers();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
