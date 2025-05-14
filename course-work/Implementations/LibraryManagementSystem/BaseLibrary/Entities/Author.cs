using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Author : BaseEntity
    {
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(1000)]
        public string? Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
