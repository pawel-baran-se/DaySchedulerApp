using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Application.Models.Identity
{
    public class CreateRoleRequest
    {
        [Required]
        public string RoleName { get; set; }
    }
}
