using Microsoft.EntityFrameworkCore;
using MyNewApiProject.Models;

namespace MyNewApiProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets for each model
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserTask> UserTasks { get; set; } // Changed from Task to UserTask

        // Fluent API configuration (if needed)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define one-to-many relationship between User and UserTask
            modelBuilder.Entity<UserTask>()
                .HasOne(ut => ut.User)  // A UserTask belongs to one User
                .WithMany(u => u.UserTasks)  // A User can have many UserTasks
                .HasForeignKey(ut => ut.UserId);  // Foreign key in UserTask

            // Define many-to-one relationship between UserTask and Category
            modelBuilder.Entity<UserTask>()
                .HasOne(ut => ut.Category)  // A UserTask belongs to one Category
                .WithMany(c => c.UserTasks)  // A Category can have many UserTasks
                .HasForeignKey(ut => ut.CategoryId);  // Foreign key in UserTask
        }
    }
}
