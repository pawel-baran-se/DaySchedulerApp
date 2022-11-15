using DaySchedulerApp.Domain.Common;


namespace DaySchedulerApp.Domain
{
    public class DaySchedule : BaseEntity //For later!!
    {
        public DateTime DateTime { get; set; }
        public IList<Assignment> Assignments { get; set; }
    }
}
