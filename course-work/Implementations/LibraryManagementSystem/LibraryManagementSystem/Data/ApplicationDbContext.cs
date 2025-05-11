using LibraryManagementSystem.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genres { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=E_LibraryManagement;Trusted_Connection=True;");
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId);

            modelBuilder.Entity<Penalty>()
                .HasOne(p => p.User)
                .WithMany(u => u.Penalties)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Penalty>()
                .HasOne(p => p.Book)
                .WithMany(b => b.Penalties)
                .HasForeignKey(p => p.BookId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reservations)
                .HasForeignKey(r => r.BookId);

            // Seed data (пример)
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Fantasy", Description = "Fantasy books" },
                new Genre { Id = 2, Name = "Science Fiction", Description = "Sci-fi books" },
                new Genre { Id = 3, Name = "Romance", Description = "Romantic novels" }
            );

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "J.K. Rowling", Biography = "British author", DateOfBirth = new DateTime(1965, 7, 31) },
                new Author { Id = 2, Name = "Isaac Asimov", Biography = "Science fiction author", DateOfBirth = new DateTime(1920, 1, 2) }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Harry Potter and the Philosopher's Stone",
                    Description = "A young wizard's journey begins.",
                    ISBN = "9780747532699",
                    BookCode = "1001",
                    PublishedDate = new DateTime(1997, 6, 26),
                    PageCount = 223,
                    Language = "English",
                    CoverImageUrl = "https://example.com/harrypotter.jpg",
                    AuthorId = 1,
                    GenreId = 1,
                    TotalCopies = 5,
                    AvailableCopies = 5
                },
                new Book
                {
                    Id = 2,
                    Title = "Foundation",
                    Description = "The story of the Galactic Empire's fall and rebirth.",
                    ISBN = "9780553293357",
                    BookCode = "1002",
                    PublishedDate = new DateTime(1951, 1, 1),
                    PageCount = 255,
                    Language = "English",
                    CoverImageUrl = "https://example.com/foundation.jpg",
                    AuthorId = 2,
                    GenreId = 2,
                    TotalCopies = 3,
                    AvailableCopies = 3
                }

            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Alice Johnson",
                    Username = "alice",
                    //Password = "hashedpassword",
                    PasswordHash = "hashedpassword",
                    Email = "alice@example.com",
                    PhoneNumber = "1234567890",
                    Address = "123 Main St",
                    Role = "User",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 2,
                    FullName = "Bob Smith",
                    Username = "bob",
                    //Password = "hashedpassword",
                    PasswordHash = "hashedpassword",
                    Email = "bob@example.com",
                    PhoneNumber = "0987654321",
                    Address = "456 Maple Ave",
                    Role = "User",
                    CreatedAt = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    UserId = 1,
                    BookId = 1,
                    DueDate = DateTime.UtcNow.AddDays(14),
                    Status = "Reserved",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Reservation
                {
                    Id = 2,
                    UserId = 2,
                    BookId = 2,
                    DueDate = DateTime.UtcNow.AddDays(14),
                    Status = "Reserved",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
                );
        }
    }
}
