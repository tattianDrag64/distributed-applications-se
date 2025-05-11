using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.API.Entities
{
    public class Genre : BaseEntity
    {
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public ICollection<Book> Books { get; set; } 
    }
}
