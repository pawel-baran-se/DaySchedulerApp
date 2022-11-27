using DaySchedulerApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Application.Contracts
{
    public interface IAssignmentRepository : IGenericRepository<Assignment>
    {
        Task<List<Assignment>> GetByUserId(string userId);
        Task<List<Assignment>> GetNotifiableForUserById(string userId);
    }
}
