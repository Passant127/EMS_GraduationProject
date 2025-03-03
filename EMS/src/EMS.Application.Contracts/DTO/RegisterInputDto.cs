using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace EMS.DTO
{
    public class RegisterInputDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$",
            ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character, and must be at least 8 characters long.")]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
