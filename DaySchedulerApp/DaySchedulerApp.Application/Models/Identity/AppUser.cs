using DaySchedulerApp.Application.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Application.Models.Identity
{
    public class AppUser
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}
