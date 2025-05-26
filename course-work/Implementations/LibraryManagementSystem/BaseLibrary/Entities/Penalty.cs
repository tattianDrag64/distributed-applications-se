using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Penalty : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookCopyId { get; set; }
        public BookCopy bookCopy { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; } = false;
        public string Reason { get; set; }
    }
}
