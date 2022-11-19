using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Application.Models.Identity
{
    public class RegistrationRequest
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword{ get; set; } = null!;
    }
}
