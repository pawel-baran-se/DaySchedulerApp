using DaySchedulerApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Domain
{
    public class Assignment : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int FrequencyInDays { get; set; }
        public bool SendNotification { get; set; }
        public DateTime LatestCompletion { get; set; }
    }
}
