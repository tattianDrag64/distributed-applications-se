using BibliotekBoklusen.Shared.DataModels;
using System.ComponentModel.DataAnnotations;

namespace BibliotekBoklusen.Shared
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Required field")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required field")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required field")]
        [EmailAddress(ErrorMessage = "No valid email address")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(32, ErrorMessage = "Password must be at least 6 characters and no more than 32.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
        public UserRole UserRole {get;set;}
}
}

