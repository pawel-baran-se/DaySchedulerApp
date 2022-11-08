using DaySchedulerApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Application.Contracts
{
    public interface IDayScheduleRepository : IGenericRepository<DaySchedule>
    {
    }
}
