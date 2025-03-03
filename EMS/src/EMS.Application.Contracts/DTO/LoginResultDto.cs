using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.DTO
{
    public class LoginResultDto
    {
        public string AccessToken { get; set; }

        public string Role { get; set; }
        public string Message { get; set; }
    }
}
