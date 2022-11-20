﻿using AutoMapper;
using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Application.Contracts.Services;
using DaySchedulerApp.Application.DTOs.Assignment;
using DaySchedulerApp.Application.DTOs.Assignment.Validators;
using DaySchedulerApp.Application.Exceptions;
using DaySchedulerApp.Application.Helper;
using DaySchedulerApp.Domain;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System.Text;

namespace DaySchedulerApp.Application.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AssignmentService> _logger;
        private readonly IAuthorizationService _authorizationService;

        public AssignmentService(IAssignmentRepository assignmentRepository,
            IMapper mapper,
            ILogger<AssignmentService> logger,
            IAuthorizationService authorizationService)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
        }

        public async Task<AssignmentDto> CreateAssignment(CreateAssignmentDto createAssignment)
        {
            var validator = new CreateAssignmentValidator();

            var result = await validator.ValidateAsync(createAssignment);

            if (!result.IsValid)
            {
                StringBuilder sb = ValidationHelper.GenerateErrorMessage(result);
                throw new ValidationException(sb.ToString());
            }
            var user = _authorizationService.GetCurrentUser();

            var assignment = _mapper.Map<Assignment>(createAssignment);

            assignment.UserId = user.Id;
            assignment.NextCompletion = DateTime.Now.AddDays(assignment.FrequencyInDays);

            await _assignmentRepository.Add(assignment);

            var assignmentDto = _mapper.Map<AssignmentDto>(assignment);

            return assignmentDto;
        }

        public async Task<bool> DeleteAssignment(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new BadRequestException("Invalid Id type!");
            }

            var assignment = await _assignmentRepository.GetById(id);

            if (assignment == null)
                throw new NotFoundException("Not Found!");

            await _assignmentRepository.Delete(id);

            return true;
        }

        public async Task<AssignmentDetailDto> GetAssignment(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new BadRequestException("Invalid Id type!");
            }

            var assignment = await _assignmentRepository.GetById(id);

            if (assignment == null)
                throw new NotFoundException("Not Found!");

            var assignmentDetailDto = _mapper.Map<AssignmentDetailDto>(assignment);

            return assignmentDetailDto;
        }

        public async Task<IEnumerable<AssignmentDto>> GetAssignments()
        {
            var user = _authorizationService.GetCurrentUser();

            var assignments = await _assignmentRepository.GetAllAsync();
            var assignmentsDto = _mapper.Map<List<AssignmentDto>>(assignments);
            _logger.LogInformation("test logging from service , PABLO!");
            return assignmentsDto;
        }

        public async Task<AssignmentDto> UpdateAssignment(string id, UpdateAssignmentDto updateAssignment)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new BadRequestException("Invalid Id type!");
            }

            var validator = new UpdateAssignmentValidator();

            var result = await validator.ValidateAsync(updateAssignment);

            if (!result.IsValid)
            {
                StringBuilder sb = ValidationHelper.GenerateErrorMessage(result);
                throw new ValidationException(sb.ToString());
            }

            var assignment = await _assignmentRepository.GetById(id);

            if (assignment == null)
                throw new NotFoundException("Not Found!");

            var updatedAssignment = _mapper.Map<Assignment>(updateAssignment);

            UpdateAssignment(assignment, updatedAssignment);

            await _assignmentRepository.Update(id, updatedAssignment);

            var assignmentDto = _mapper.Map<AssignmentDto>(assignment);

            return assignmentDto;
        }

        private static void UpdateAssignment(Assignment assignment, Assignment updatedAssignment)
        {
            updatedAssignment.Id = assignment.Id;
            updatedAssignment.Name = assignment.Name;
            updatedAssignment.LatestCompletion = assignment.LatestCompletion;
            updatedAssignment.UserId = assignment.UserId;
        }
    }
}