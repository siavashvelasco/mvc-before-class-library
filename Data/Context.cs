using Microsoft.EntityFrameworkCore;
using MVC23._10._1403.Models;


public class Context : DbContext
{
   public DbSet<Category> _categories { get; set; }
    public Context(DbContextOptions<Context> options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Ali", Order = 100 });
    }
}

