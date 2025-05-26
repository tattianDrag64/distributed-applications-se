using BaseLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class BookDTO
    {
        //public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PageCount { get; set; }
        public string Language { get; set; }
        public string CoverImageUrl { get; set; }
        public int AuthorId { get; set; } 
        public int GenreId { get; set; } 
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        //public ICollection<BookCopy> BookCopies { get; set; }
        //public ICollection<Review> Reviews { get; set; }

    }
}
