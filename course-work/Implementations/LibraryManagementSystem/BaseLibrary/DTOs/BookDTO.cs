using BaseLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public int BookCode { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PageCount { get; set; }
        public string Language { get; set; }
        public string CoverImageUrl { get; set; }
        public string AuthorName { get; set; } = null!;
        public string GenreName { get; set; } = null!;
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
    }
}
