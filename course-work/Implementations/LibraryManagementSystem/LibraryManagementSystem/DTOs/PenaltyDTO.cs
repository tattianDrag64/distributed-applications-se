namespace LibraryManagementSystem.API.DTOs
{
    public class PenaltyDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
