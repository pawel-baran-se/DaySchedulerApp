using DaySchedulerApp.Application.DTOs.Assignment;
using DaySchedulerApp.Domain;

namespace DaySchedulerApp.Application.Contracts.Services
{
    public interface IAssignmentService
    {
        Task<AssignmentDto> CreateAssignment(CreateAssignmentDto createAssignment);

        Task<AssignmentDto> UpdateAssignment(string id, UpdateAssignmentDto updateAssignment);

        Task<bool> DeleteAssignment(string id);

        Task<AssignmentDetailDto> GetAssignment(string id);

        Task<IEnumerable<AssignmentDto>> GetAssignments();
    }
}