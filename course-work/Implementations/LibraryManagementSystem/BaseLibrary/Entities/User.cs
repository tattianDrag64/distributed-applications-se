using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class User : BaseEntity
    {
        //[MaxLength(50)]
        public required string FullName { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        public required string Username { get; set; }
        //public required string Password { get; set; }
        public required string PasswordHash { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }

        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int TotalReadBooks { get; set; } = 0;

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Penalty> Penalties { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
