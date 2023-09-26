using Microsoft.EntityFrameworkCore;
using TestCoreApp.Models;

namespace TestCoreApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Employee> employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "select category" }, 
                new Category { Id = 2, Name = "Electronics" }, 
                new Category { Id = 3, Name = "lap" },
                new Category { Id = 4, Name = "mobile" });
            base.OnModelCreating(modelBuilder);
        }
        

    }
}
