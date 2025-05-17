using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
namespace BaseLibrary.DTOs
{
    public class LoginDTO : AccountBaseDTO
    {
        [Required(ErrorMessage = "Error Login!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Error Login!")]
        public string Password { get; set; } = string.Empty;
    }
}
