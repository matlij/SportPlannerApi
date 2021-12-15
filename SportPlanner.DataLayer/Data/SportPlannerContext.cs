using Microsoft.EntityFrameworkCore;
using SportPlanner.DataLayer.Models;

namespace SportPlanner.DataLayer.Data
{
    public class SportPlannerContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EventUser> EventUsers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public SportPlannerContext(DbContextOptions<SportPlannerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder
                .Entity<EventUser>()
                .HasKey(e => new { e.EventId, e.UserId });
            modelBuilder.Entity<EventUser>()
                .HasOne(eu => eu.Event)
                .WithMany(e => e.Users)
                .HasForeignKey(eu => eu.EventId);
            modelBuilder.Entity<EventUser>()
                .HasOne(eu => eu.User)
                .WithMany(u => u.Events)
                .HasForeignKey(eu => eu.UserId);
        }
    }
}
