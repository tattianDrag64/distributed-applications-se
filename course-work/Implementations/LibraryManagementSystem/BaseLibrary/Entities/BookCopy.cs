using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class BookCopy 
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        [MaxLength(10)]
        public required string BookCode { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int? BorrowerId { get; set; }
        public User? Borrower { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Penalty> Penalties { get; set; }
    }
}
