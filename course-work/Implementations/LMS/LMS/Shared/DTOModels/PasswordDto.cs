using System.ComponentModel.DataAnnotations;

namespace BibliotekBoklusen.Shared
{
    public class PasswordDto
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(32, ErrorMessage = "Password must be at least 6 characters and no more than 32.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "Password does not match")]
        public string? NewPasswordConfirmed { get; set; }
    }
}
