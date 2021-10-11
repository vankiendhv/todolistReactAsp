using System;
using Microsoft.EntityFrameworkCore;

namespace todolistReactAsp.Connection
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
                : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        public DbSet<Models.SendGmail> SendGmail { get; set; }
    }
}