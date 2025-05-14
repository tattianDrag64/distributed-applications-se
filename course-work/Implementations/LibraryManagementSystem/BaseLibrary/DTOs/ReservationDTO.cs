namespace BaseLibrary.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
