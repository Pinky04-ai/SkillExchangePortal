using Microsoft.EntityFrameworkCore;
using SkillExchange.DAL.Entities;

namespace SkillExchange.DAL.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<ContentItem> Contents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.FullName).IsRequired().HasMaxLength(200);
            });

            builder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(50);
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            builder.Entity<ContentItem>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Title).IsRequired().HasMaxLength(200);

                entity.HasOne(c => c.User)
                      .WithMany(u => u.Contents)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Category)
                      .WithMany(cat => cat.Contents)
                      .HasForeignKey(c => c.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Feedback>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Comment).HasMaxLength(1000);

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

                entity.HasOne(m => m.FromUser)
                      .WithMany(u => u.SentMessages)
                      .HasForeignKey(m => m.FromUserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.ToUser)
                      .WithMany(u => u.ReceivedMessages)
                      .HasForeignKey(m => m.ToUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}

