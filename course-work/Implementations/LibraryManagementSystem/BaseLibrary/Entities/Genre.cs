using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
