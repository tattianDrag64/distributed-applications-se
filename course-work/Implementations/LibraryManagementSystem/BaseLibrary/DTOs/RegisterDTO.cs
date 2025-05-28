using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaseLibrary.Utility.SD;

namespace BaseLibrary.DTOs
{
    public class RegisterDTO : AccountBaseDTO
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? FullName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public Role role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
