using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Book 
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Title { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public long ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PageCount { get; set; }

        [MaxLength(50)]
        public string Language { get; set; }
        public string CoverImageUrl { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
        public int TotalCopies { get; set; } = 1;
        public int AvailableCopies { get; set; } = 1;
        public ICollection<BookCopy> BookCopies { get; set; } 
      
        public ICollection<Review> Reviews { get; set; }
        
    }
}
