using Courses.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Courses.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Course>().HasMany(u => u.Subjects).WithOne(x => x.Course);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
