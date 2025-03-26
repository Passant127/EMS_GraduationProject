using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.DTO
{
    public class LoginDto
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }

    public class UserDataDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public float? Weight { get; set; }
        public float? Height { get; set; }
        public DateTime? BOD { get; set; }
        public string? Address { get; set; }
    }

}
