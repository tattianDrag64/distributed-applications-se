using BaseLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAlive { get; set; } = true;
        public DateTime? DateOfDeath { get; set; }
        //public ICollection<Book> Books { get; set; }
        //public ICollection<Book> Books { get; set; }
    }
}
