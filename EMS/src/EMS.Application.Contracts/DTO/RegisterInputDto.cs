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

        public float? Weight { get; set; }
        public float? Height { get; set; }
        public DateTime? BOD { get; set; }
        public string? Address { get; set; }
    }

    public class UpdateProfileDto
    {
       
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BOD { get; set; }

        public float? Height { get; set; }

        public float? Weight { get; set; }

        public string Address { get; set; }
    }

}
