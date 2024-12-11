using Api_intro.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<GroupStudent> GroupStudent { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

      
    }
}
