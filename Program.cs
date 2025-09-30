
using Amazon;
using Amazon.S3;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using SkillExchange.BAL.Interface;
using SkillExchange.BAL.Managers;
using SkillExchange.BAL.Services;
using SkillExchange.Core.Entities;
using SkillExchange.DAL.Interfaces;
using SkillExchange.DAL.Repositories;
using SkillExchange.DAL.Repository;
using SkillExchange.Infrastructure.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IContentRepository, ContentRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<ICategoryManager, CategoryManager>();
builder.Services.AddScoped<IContentManager, ContentManager>();
builder.Services.AddScoped<IFeedbackManager, FeedbackManager>();
builder.Services.AddScoped<IAdminManager, AdminManager>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IApproveManager, ApproveManager>();
builder.Services.AddScoped<IApproveRepository, ApproveRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<S3Service>();

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = builder.Configuration.GetSection("AWS");
    var accessKey = config["AccessKey"];
    var secretKey = config["SecretKey"];
    var region = RegionEndpoint.GetBySystemName(config["Region"]);
    return new AmazonS3Client(accessKey, secretKey, region);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(opt =>
//    {
//        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
//        opt.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidateAudience = true,
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(key),
//            ValidateLifetime = true
//        };
//    });

//builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkillExchange API", Version = "v1" });


//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Enter 'Bearer' followed by your JWT token"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] { }
//        }
//    });
//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors("AllowBlazor");
app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
