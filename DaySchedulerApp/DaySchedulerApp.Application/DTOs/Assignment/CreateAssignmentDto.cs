using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Application.DTOs.Assignment
{
    public class CreateAssignmentDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int FrequencyInDays { get; set; }
        public bool SendNotification { get; set; }
    }
}
