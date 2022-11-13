using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Persistance.Configurations
{
    public class DaySchedulerDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string AssignmentsCollectionName { get; set; } = null!;
        public string DaySchedulesCollectionName { get; set; } = null!;
    }
}
