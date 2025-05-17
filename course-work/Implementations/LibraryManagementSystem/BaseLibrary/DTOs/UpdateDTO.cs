using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs
{
    public class UpdateDTO : AccountBaseDTO
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [EmailAddress]
        [Required]
        public string OldPassword { get; set; }

        [StringLength(32, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? NewPasswordConfirmed { get; set; }
    }
}
