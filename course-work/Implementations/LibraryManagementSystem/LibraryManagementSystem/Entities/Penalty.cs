namespace LibraryManagementSystem.API.Entities
{
    public class Penalty : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; } = false;
        public string Reason { get; set; }
    }
}
