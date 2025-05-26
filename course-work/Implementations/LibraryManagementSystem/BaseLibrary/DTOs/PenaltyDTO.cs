namespace BaseLibrary.DTOs
{
    public class PenaltyDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; } = false;
        public string Reason { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public int BookCopyId { get; set; }
    }
}
