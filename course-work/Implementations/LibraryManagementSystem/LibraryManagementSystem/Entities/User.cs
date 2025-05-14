using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.API.Entities
{
    public class User :BaseEntity
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

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Penalty> Penalties { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
