using DaySchedulerApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Domain
{
    public class DaySchedule : BaseEntity
    {
        public DateTime DateTime { get; set; }
        public IList<Assignment> Assignments { get; set; }
    }
}
