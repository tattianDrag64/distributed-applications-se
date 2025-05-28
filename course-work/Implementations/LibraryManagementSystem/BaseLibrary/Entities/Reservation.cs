using BaseLibrary.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BaseLibrary.Utility.SD;

namespace BaseLibrary.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; }

        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        [MaxLength(10)]
        public ReservationStatus Status { get; set; }
        public bool IsReturned { get; set; }
    }
}
