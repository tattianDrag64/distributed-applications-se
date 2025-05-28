using BaseLibrary.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;
using static BaseLibrary.Utility.SD;
using static System.Net.WebRequestMethods;

namespace ServerLibrary.Data.AppDbCon
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
        public DbSet<EventLibrary> Events { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }

        public static ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-CNVLSQN;Database=LibraryManagementSystemDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === USER ===  
            modelBuilder.Entity<User>(entity =>
            {
                //entity.HasKey(u => u.Id);

                entity.Property(u => u.FullName).IsRequired();
                entity.Property(u => u.Address).HasMaxLength(50);
                entity.Property(u => u.Username).IsRequired();
                entity.Property(u => u.Email).HasMaxLength(100);
                entity.Property(u => u.ImageUrl);
                entity.Property(u => u.Description);
            });

            // === AUTHOR ===  
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Name).IsRequired().HasMaxLength(50);
                entity.Property(a => a.Biography).HasMaxLength(1000);
                entity.Property(a => a.DateOfBirth).IsRequired();
                entity.Property(a => a.ImageUrl);
            });

            // === GENRE ===  
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(g => g.Id);

                entity.Property(g => g.Name).HasMaxLength(20);
                entity.Property(g => g.Description).HasMaxLength(1000);
            });

            // === BOOK ===  
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.Title).IsRequired().HasMaxLength(100);
                entity.Property(b => b.Description).HasMaxLength(1000);
                entity.Property(b => b.Language).HasMaxLength(50);
                entity.Property(b => b.CoverImageUrl);

                entity.HasOne(b => b.Author)
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Genre)
                    .WithMany(g => g.Books)
                    .HasForeignKey(b => b.GenreId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // === BOOKCOPY ===  
            modelBuilder.Entity<BookCopy>(entity =>
            {
                entity.HasKey(bc => bc.Id);

                entity.Property(bc => bc.BookCode).IsRequired().HasMaxLength(10);

                entity.HasOne(bc => bc.Book)
                    .WithMany(b => b.BookCopies)
                    .HasForeignKey(bc => bc.BookId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(bc => bc.Borrower)
                    .WithMany()
                    .HasForeignKey(bc => bc.BorrowerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // === RESERVATION ===  
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.ReservationDate).IsRequired();
                entity.Property(r => r.DueDate).IsRequired();
                entity.Property(r => r.Status)
                    .HasConversion<string>()
                    .HasMaxLength(10);

                entity.HasOne(r => r.User)
                    .WithMany(u => u.Reservations)
                    .HasForeignKey(r => r.UserId);

                entity.HasOne(r => r.BookCopy)
                .WithMany(bc => bc.Reservations)
                .HasForeignKey(r => r.BookCopyId);
            });

            // === REVIEW ===  
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Rating).IsRequired();
                entity.Property(r => r.Comment);

                entity.HasOne(r => r.Book)
                    .WithMany(b => b.Reviews)
                    .HasForeignKey(r => r.BookId);

                entity.HasOne(r => r.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(r => r.UserId);
            });

            // === PENALTY ===  
            modelBuilder.Entity<Penalty>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.DueDate);
                entity.Property(p => p.Amount);
                entity.Property(p => p.Reason);

                entity.HasOne(p => p.User)
                    .WithMany(u => u.Penalties)
                    .HasForeignKey(p => p.UserId);

                entity.HasOne(p => p.bookCopy)
                    .WithMany(b => b.Penalties)
                    .HasForeignKey(p => p.BookCopyId);

                entity.Property(p => p.Amount).HasColumnType("decimal(10,2)");
            });

            // === EVENT ===  
            modelBuilder.Entity<EventLibrary>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description);

                entity.HasOne(e => e.Organizer)
                    .WithMany()
                    .HasForeignKey(e => e.OrganizerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed Data  
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    Name = "Gabriel Garcia Marquez",
                    Biography = "Colombian author, Nobel Prize winner.",
                    DateOfBirth = new DateTime(1927, 3, 6),
                    IsAlive = false,
                    DateOfDeath = new DateTime(2014, 4, 17),
                    ImageUrl = null
                }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre
                {
                    Id = 1,
                    Name = "Novel",
                    Description = "A long narrative fictional work."
                }
            );



            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = -1,
                    FullName = "John Smith",
                    Username = "john123",
                    Email = "john@example.com",
                    Address = "New York, USA",
                    role = Role.Member,
                    TotalReadBooks = 0,
                    DateOfBirth = new DateTime(1990, 1, 1),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)

                }

            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "One Hundred Years of Solitude",
                    Description = "Magical realism and family saga.",
                    ISBN = 1234567890123,
                    PublishedDate = new DateTime(1967, 5, 30),
                    PageCount = 417,
                    Language = "Spanish",
                    CoverImageUrl = "https://www.amazon.in/Hundred-Years-Solitude-International-Writers/dp/0140157514",
                    AuthorId = 1,
                    GenreId = 1,
                    TotalCopies = 3,
                    AvailableCopies = 3
                }
            );

            modelBuilder.Entity<BookCopy>().HasData(
                new BookCopy
                {
                    Id = 1,
                    BookId = 1,
                    BookCode = "BC001",
                    IsAvailable = true
                },
                new BookCopy
                {
                    Id = 2,
                    BookId = 1,
                    BookCode = "BC002",
                    IsAvailable = true
                }
            );

            modelBuilder.Entity<EventLibrary>().HasData(
                new EventLibrary
                {
                    Id = 1,
                    Title = "Literature Seminar",
                    Description = "Discussion on Latin American literature.",
                    EventDate = new DateTime(2025, 6, 15),
                    OrganizerId = -1
                }
            );
            modelBuilder.Entity<Genre>()
              .HasData(
                  new Genre() { Id = 9, Name = "Crime", Description = "Books that involve solving crimes or mysteries." },
                  new Genre() { Id = 2, Name = "Feelgood", Description = "Books that evoke positive emotions and comfort." },
                  new Genre() { Id = 3, Name = "Biography", Description = "Books that narrate the life story of a person." },
                  new Genre() { Id = 4, Name = "Thriller", Description = "Books filled with suspense and excitement." },
                  new Genre() { Id = 5, Name = "Novels", Description = "Fictional narrative works of significant length." },
                  new Genre() { Id = 6, Name = "History", Description = "Books that explore historical events and periods." },
                  new Genre() { Id = 7, Name = "Fantasy & SciFi", Description = "Books that involve imaginative worlds or futuristic science." },
                  new Genre() { Id = 8, Name = "Non-Fiction", Description = "Books based on factual information and real events." }
              );

            modelBuilder.Entity<User>()
                .HasData(new User() { Id = 1, FullName = "AdminAdmin", Email = "admin@admin.com", Address = "Plovdiv, Bulgaria", Username = "Admin", CreatedAt = DateTime.Now, role = Role.Admin });


            //modelBuilder.Entity<Reservation>().HasData(
            //    new Reservation
            //    {
            //        Id = 1,
            //        UserId = 1,
            //        BookCopyId = 1,
            //        ReservationDate = new DateTime(2025, 5, 20),
            //        DueDate = new DateTime(2025, 6, 3),
            //        Status = ReservationStatus.Active,
            //        IsReturned = false
            //    }
            //);

            //modelBuilder.Entity<Review>().HasData(
            //    new Review
            //    {
            //        Id = 1,
            //        BookId = 1,
            //        UserId = -1,
            //        Rating = 5,
            //        Comment = "An outstanding novel with beautiful prose and deep themes.",
            //        CreatedAt = DateTime.UtcNow
            //    }
            //);

            //modelBuilder.Entity<Penalty>().HasData(
            //    new Penalty
            //    {
            //        Id = 1,
            //        UserId = -1,
            //        BookCopyId = 1,
            //        DueDate = new DateTime(2025, 5, 10),
            //        Amount = 5.00m,
            //        IsPaid = false,
            //        Reason = "Late return of borrowed book"
            //    }
            //);
        }
    }
}
