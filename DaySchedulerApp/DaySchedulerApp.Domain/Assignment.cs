using DaySchedulerApp.Domain.Common;

namespace DaySchedulerApp.Domain
{
    public class Assignment : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int FrequencyInDays { get; set; }
        public bool SendNotification { get; set; }
        public DateTime? LatestCompletion { get; set; }
        public DateTime? NextCompletion { get; set; }
        public string UserId { get; set; }
    }
}