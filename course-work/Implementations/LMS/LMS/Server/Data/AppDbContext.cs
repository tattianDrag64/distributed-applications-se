using BibliotekBoklusen.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotekBoklusen.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Seminarium> Seminariums { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductCopy> productCopies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Category>()
                       .HasData(new Category() { Id = 1, CategoryName = "Detectives" },
                                new Category() { Id = 2, CategoryName = "Feelgood" },
                                new Category() { Id = 3, CategoryName = "Biography" },
                                new Category() { Id = 4, CategoryName = "Suspense" },
                                new Category() { Id = 5, CategoryName = "Novels" },
                                new Category() { Id = 6, CategoryName = "History" },
                                new Category() { Id = 7, CategoryName = "Fantasy & SciFi" },
                                new Category() { Id = 8, CategoryName = "Facts" });

            modelBuilder.Entity<User>()
                .HasData(new User() { Id = 1, Email = "admin@admin.com", Created = DateTime.Now, UserRole = UserRole.Admin });

        }
    }
}
