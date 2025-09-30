using Microsoft.EntityFrameworkCore;
using SkillExchange.Core.DTO;
using SkillExchange.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static SkillExchange.Core.Enum.Enum;

namespace SkillExchange.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(200);
                entity.HasIndex(e => e.Email).IsUnique();
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });
            modelBuilder.Entity<Content>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.ContentType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.FileUrl).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Status)
                      .HasConversion<string>();
                entity.HasOne(c => c.UploadedBy)
                       .WithMany(u => u.UploadedContents)
                .HasForeignKey(c => c.UploadedById)
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(c => c.ApprovedBy)
                .WithMany(u => u.ApprovedContents)
                .HasForeignKey(c => c.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Comment).HasMaxLength(1000);
                entity.Property(e => e.Rating).IsRequired();
                entity.HasIndex(e => new { e.UserId, e.ContentId }).IsUnique();
                entity.Property(f => f.ContentTitle).HasColumnName("ContentTitle");
                entity.Property(f => f.UserName).HasColumnName("UserName");
                entity.Ignore(f => f.Content);
                entity.Ignore(f => f.User);

            });
            modelBuilder.Entity<User>().HasData(
            new User
            {
                 Id = 1,
                 FirstName = "Admin",
                 LastName = "User",
                 Email = "admin@skillexchange.com",
                 PasswordHash = "Admin@123",
                 Role = UserRole.Admin,
                 Status = UserStatus.Verified,
                 CreatedAt = new DateTime(2024, 01, 01),
                 VerifiedAt = new DateTime(2024, 01, 01)
            }
            );
            modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Programming",
                Description = "Programming languages and development skills",
                IsActive = true,
                SortOrder = 1,
                CreatedById = 1,
                CreatedAt = new DateTime(2024, 01, 01)
            }
        );
        }
    }
}