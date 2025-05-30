using System.ComponentModel.DataAnnotations;

namespace BibliotekBoklusen.Shared
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Required field")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required field")]
        public string Password { get; set; } = string.Empty;
    }
}
