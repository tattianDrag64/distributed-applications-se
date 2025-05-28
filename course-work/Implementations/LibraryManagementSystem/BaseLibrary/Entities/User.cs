using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static BaseLibrary.Utility.SD;

namespace BaseLibrary.Entities
{
    // Changed to inherit from IdentityUser and removed the generic type argument  
    public class User 
    {
        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Role role { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int TotalReadBooks { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Penalty> Penalties { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<EventLibrary> Events { get; set; }
        public bool IsActive { get; set; }
    }
}
