using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Reservation : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        [MaxLength(10)]
        public string Status { get; set; }
    }
}
