using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Application.Models.Identity
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
