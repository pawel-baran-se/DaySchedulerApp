using AutoMapper;
using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Application.Contracts.Services;
using DaySchedulerApp.Application.DTOs.Assignment;
using DaySchedulerApp.Domain;

namespace DaySchedulerApp.Application.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;

        public AssignmentService(IAssignmentRepository assignmentRepository,
            IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
        }

        public async Task<AssignmentDto> CreateAssignment(CreateAssignmentDto createAssignment)
        {
            var assignment = _mapper.Map<Assignment>(createAssignment);
            await _assignmentRepository.Add(assignment);
            var assignmentDto = _mapper.Map<AssignmentDto>(assignment);
            return assignmentDto;
        }

        public async Task<bool> DeleteAssignment(string id)
        {
            await _assignmentRepository.Delete(id);
            return true;
        }

        public async Task<AssignmentDetailDto> GetAssignment(string id)
        {
            var assignment = await _assignmentRepository.GetById(id);
            var assignmentDetailDto = _mapper.Map<AssignmentDetailDto>(assignment);
            return assignmentDetailDto;
        }

        public async Task<IEnumerable<AssignmentDto>> GetAssignments()
        {
            var assignments = await _assignmentRepository.GetAllAsync();
            var assignmentsDto = _mapper.Map<List<AssignmentDto>>(assignments);
            return assignmentsDto;
        }

        public async Task<AssignmentDto> UpdateAssignment(string id, UpdateAssignmentDto updateAssignment)
        {
            var assignment = _mapper.Map<Assignment>(updateAssignment);
            await _assignmentRepository.Update(assignment);
            var assignmentDto = _mapper.Map<AssignmentDto>(assignment);
            return assignmentDto;
        }
    }
}