using Microsoft.EntityFrameworkCore;
using SkillExchange.DAL.Entities;
using System.Drawing.Text;
using static SkillExchange.DAL.Enums.Enum;
namespace SkillExchange.DAL.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContentItem> ContentItems { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(u => u.Password)
                      .IsRequired();
                entity.Property(u => u.FullName)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(u => u.Status)
                      .HasConversion<int>()
                      .IsRequired();
                entity.Property(u => u.CreatedAt)
                      .IsRequired();
                entity.HasMany(u => u.Contents)
                      .WithOne(c => c.User)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(u => u.Feedbacks)
                      .WithOne(f => f.User)
                      .HasForeignKey(f => f.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(u => u.SentMessages)
                      .WithOne(m => m.FromUser)
                      .HasForeignKey(m => m.FromUserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(u => u.ReceivedMessages)
                      .WithOne(m => m.ToUser)
                      .HasForeignKey(m => m.ToUserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(u => u.UserRoles)
                      .WithOne(ur => ur.User)
                      .HasForeignKey(ur => ur.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.RoleName)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(r => r.RoleType)
                      .HasConversion<int>()
                      .IsRequired();
                entity.Property(r => r.CreatedAt)
                      .IsRequired();
                entity.HasMany(r => r.UserRoles)
                      .WithOne(ur => ur.Role)
                      .HasForeignKey(ur => ur.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserId);
                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleId);
            });

            builder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasMany(c => c.Contents)
                      .WithOne(cnt => cnt.Category)
                      .HasForeignKey(cnt => cnt.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<ContentItem>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Title)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(c => c.FileUrl)
                      .IsRequired();
                entity.Property(c => c.Status)
                      .HasConversion<int>()
                      .IsRequired();
                entity.Property(c => c.CreatedAt)
                      .IsRequired();
                entity.Property(c => c.UpdatedAt)
                      .IsRequired();
                entity.HasOne(c => c.User)
                      .WithMany(u => u.Contents)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(c => c.Category)
                      .WithMany(cat => cat.Contents)
                      .HasForeignKey(c => c.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(c => c.Feedbacks)
                      .WithOne(f => f.Content)
                      .HasForeignKey(f => f.ContentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Feedback>(entity =>
            {
                entity.HasKey(f => f.Id);

                entity.Property(f => f.Comment)
                      .HasMaxLength(1000);

                entity.Property(f => f.Rating)
                      .HasConversion<int>()
                      .IsRequired();

                entity.Property(f => f.CreatedAt)
                      .IsRequired();
                entity.HasOne(f => f.User)
                      .WithMany(u => u.Feedbacks)
                      .HasForeignKey(f => f.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(f => f.Content)
                      .WithMany(c => c.Feedbacks)
                      .HasForeignKey(f => f.ContentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Message>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Content)
                      .IsRequired()
                      .HasMaxLength(2000);
                entity.Property(m => m.SentAt)
                      .IsRequired();
                entity.Property(m => m.Status)
                      .HasConversion<int>()
                      .IsRequired();
                entity.Property(m => m.IsRead)
                      .IsRequired()
                      .HasDefaultValue(false);
                entity.HasOne(m => m.FromUser)
                      .WithMany()
                      .HasForeignKey(m => m.FromUserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(m => m.ToUser)
                      .WithMany()
                      .HasForeignKey(m => m.ToUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Role>().HasData(
               new Role { Id = 1, RoleName = "Admin", RoleType = UserRoleType.Admin, CreatedAt = DateTime.UtcNow },
               new Role { Id = 2, RoleName = "User", RoleType = UserRoleType.User, CreatedAt = DateTime.UtcNow }
            );
            builder.Entity<AppUser>().HasData(
               new AppUser
               {
                   Id = 1,
                   FullName = "Admin User",
                   Email = "admin@skillportal.com",
                   Password = "hashed_admin_pw",
                   Status = UserStatus.Verified,
                   CreatedAt = DateTime.UtcNow
               },
               new AppUser
               {
                   Id = 2,
                   FullName = "John Doe",
                   Email = "john@skillportal.com",
                   Password = "hashed_john_pw",
                   Status = UserStatus.Verified,
                   CreatedAt = DateTime.UtcNow
               },
               new AppUser
               {
                   Id = 3,
                   FullName = "Jane Smith",
                   Email = "jane@skillportal.com",
                   Password = "hashed_jane_pw",
                   Status = UserStatus.Suspended,
                   CreatedAt = DateTime.UtcNow
               }
            );
            builder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 },
                new UserRole { UserId = 2, RoleId = 2 },
                new UserRole { UserId = 3, RoleId = 2 }
            );
            builder.Entity<Category>().HasData(
              new Category { Id = 1, Name = "Programming" },
              new Category { Id = 2, Name = "Design" },
              new Category { Id = 3, Name = "Music" }
            );
            builder.Entity<ContentItem>().HasData(
               new ContentItem
               {
                   Id = 1,
                   Title = "Learn C# Basics",
                   FileUrl = "C:\\Users\\ASUS\\Documents\\Internship\\SKILL EXCHANGE PORTAL\\SkillExchangePortal\\SkillExchange.DAL\\Files\\lecture1424354156.pdf",
                   UserId = 2,
                   CategoryId = 1,
                   Status = ContentStatus.Approved,
                   CreatedAt = DateTime.UtcNow,
                   UpdatedAt = DateTime.UtcNow
               },
               new ContentItem
               {
                   Id = 2,
                   Title = "UI/UX Design Principles",
                   FileUrl = "C:\\Users\\ASUS\\Documents\\Internship\\SKILL EXCHANGE PORTAL\\SkillExchangePortal\\SkillExchange.DAL\\Files\\09-UX.pdf",
                   UserId = 3,
                   CategoryId = 2,
                   Status = ContentStatus.PendingApproval,
                   CreatedAt = DateTime.UtcNow,
                   UpdatedAt = DateTime.UtcNow
               }
            );
            builder.Entity<Feedback>().HasData(
               new Feedback
               {
                   Id = 1,
                   UserId = 3,
                   ContentId = 1,
                   Rating = FeedbackRating.Good,
                   Comment = "Good content!",
                   CreatedAt = DateTime.UtcNow
               },
               new Feedback
               {
                   Id = 2,
                   UserId = 2,
                   ContentId = 2,
                   Rating = FeedbackRating.Average,
                   Comment = "Not a better content!",
                   CreatedAt = DateTime.UtcNow
               }
            );
            builder.Entity<Message>().HasData(
                new Message
                {
                    Id = 1,
                    FromUserId = 2,
                    ToUserId = 3,
                    Content = "Hello Jane! Welcome to the portal.",
                    Status = MessageStatus.Sent,
                    IsRead = false,
                    SentAt = DateTime.UtcNow
                },
                new Message
                {
                    Id = 2,
                    FromUserId = 3,
                    ToUserId = 2,
                    Content = "Hi John, thank you!",
                    Status = MessageStatus.Read,
                    IsRead = true,
                    SentAt = DateTime.UtcNow
                }
            );
        }
    }
}

