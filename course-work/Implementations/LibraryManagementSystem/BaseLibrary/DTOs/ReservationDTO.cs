namespace BaseLibrary.DTOs
{
    public class ReservationDTO
    {
        //public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public int Username { get; set; }
        public int BookCopy { get; set; }
        public bool IsReturned { get; set; }
    }
}
