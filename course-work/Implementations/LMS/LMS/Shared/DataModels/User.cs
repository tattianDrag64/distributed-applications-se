using BibliotekBoklusen.Shared.DataModels;
using System.ComponentModel.DataAnnotations;

namespace BibliotekBoklusen.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        //[Required(ErrorMessage = "Required Field")]
        public string Email { get; set; } = String.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public UserRole UserRole { get; set; }
    }
}
