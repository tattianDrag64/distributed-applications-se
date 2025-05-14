using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.API.Entities
{
    public class Book : BaseEntity
    {
        [MaxLength(100)]
        public required string Title { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public long ISBN { get; set; }
        [MaxLength(10)]
        public required string BookCode { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PageCount { get; set; }

        [MaxLength(50)]
        public string Language { get; set; }
        public string CoverImageUrl { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int TotalCopies { get; set; } = 1;
        public int AvailableCopies { get; set; } = 1;
        // public ICollection<User> BorrowedUsers { get; set; } = new List<User>();
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Penalty> Penalties { get; set; }
    }
}
