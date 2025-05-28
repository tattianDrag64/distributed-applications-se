using BaseLibrary.Entities;
using static BaseLibrary.Utility.SD;

namespace BaseLibrary.DTOs
{
    public class UserDTO
    {
        //public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Role role { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }

        //public ICollection<Review> Reviews { get; set; }
        //public ICollection<Penalty> Penalties { get; set; }
        //public ICollection<Reservation> Reservations { get; set; }
        //public ICollection<Event> Events { get; set; }
    }
}
