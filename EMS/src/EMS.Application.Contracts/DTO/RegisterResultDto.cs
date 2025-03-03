using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace EMS.DTO
{
    public class RegisterResultDto
    {
        public string AccessToken { get; set; }
        public string Message { get; set; }

    }
}
