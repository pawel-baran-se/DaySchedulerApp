using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Domain;
using DaySchedulerApp.Persistance.Configurations;
using Microsoft.Extensions.Options;

namespace DaySchedulerApp.Persistance.Repositories
{
    public class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(IOptions<DaySchedulerDatabaseSettings> daySchedulerDatabaseSettings)
            : base(daySchedulerDatabaseSettings, daySchedulerDatabaseSettings.Value.AssignmentsCollectionName)
        {
        }
    }
}