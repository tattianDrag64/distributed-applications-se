using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [DataType(DataType.Password)]
        [Required]
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }

    }
}
