using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using todolistReactAsp.Models;
using Microsoft.AspNetCore.Identity;

namespace todolistReactAsp.Connection
{
    public class ApplicationContext : IdentityDbContext<User, Roles, int>
    {
        public ApplicationContext(DbContextOptions options)
                : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            modelBuilder.Entity<Models.TagJob>()
            .HasOne(b => b.Tag)
            .WithMany(ba => ba.TagJobs)
            .HasForeignKey(bi => bi.TagId);

            modelBuilder.Entity<Models.TagJob>()
            .HasOne(b => b.Job)
            .WithMany(ba => ba.TagJob)
            .HasForeignKey(bi => bi.JobId);
        }
        public DbSet<Models.User> User { get; set; }
        public DbSet<Models.Category> Category { get; set; }
        public DbSet<Models.Tag> Tag { get; set; }
        public DbSet<Models.Job> Job { get; set; }
        public DbSet<Models.Notification> Notification { get; set; }
        public DbSet<Models.TagJob> TagJob { get; set; }
        public DbSet<Models.TokenNotification> TokenNotification { get; set; }
        public DbSet<Models.SendGmail> SendGmail { get; set; }
    }
}