﻿using AutoMapper;
using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Application.Contracts.Infrastructure;
using DaySchedulerApp.Application.Contracts.Services;
using DaySchedulerApp.Application.DTOs.Assignment;
using DaySchedulerApp.Application.DTOs.Assignment.Validators;
using DaySchedulerApp.Application.Exceptions;
using DaySchedulerApp.Application.Helper;
using DaySchedulerApp.Application.Models.Email;
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
        private readonly IEmailSender _emailSender;

        public AssignmentService(IAssignmentRepository assignmentRepository,
            IMapper mapper,
            ILogger<AssignmentService> logger,
            IAuthorizationService authorizationService,
            IEmailSender emailSender)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _emailSender = emailSender;
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

            if (!_authorizationService.ValidateUserId(assignment.UserId))
                throw new BadRequestException("Invalid Request!");

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

        public async Task<IEnumerable<AssignmentListDto>> GetAssignments()
        {
            var assignments = new List<Assignment>();
            var user = _authorizationService.GetCurrentUser();

            if(user.Role == Enum.UserRole.ADMINISTRATOR)
                assignments = await _assignmentRepository.GetAllAsync();

            if (user.Role == Enum.UserRole.USER)
                assignments = await _assignmentRepository.GetByUserId(user.Id);
            await UpdateCompletionDate(assignments);
            var assignmentsDto = _mapper.Map<List<AssignmentListDto>>(assignments);
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

            if (!_authorizationService.ValidateUserId(assignment.UserId))
                throw new BadRequestException("Invalid Request!");

            if (ValidationHelper.StringInputValidation(updateAssignment.Description))
            {
                assignment.Description = updateAssignment.Description;
            }

            if (updateAssignment.FrequencyInDays != assignment.FrequencyInDays)
            {
                assignment.FrequencyInDays = updateAssignment.FrequencyInDays;
            }

            await _assignmentRepository.Update(id, assignment);

            var assignmentDto = _mapper.Map<AssignmentDto>(assignment);

            return assignmentDto;
        }

        public async Task<AssignmentDto> UpdateNoificationSettings(string id, ChangeNotificationSettingsDto updateAssignment)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                throw new BadRequestException("Invalid Id type!");
            }

            var assignment = await _assignmentRepository.GetById(id);

            if (assignment == null)
                throw new NotFoundException("Not Found!");

            if (!_authorizationService.ValidateUserId(assignment.UserId))
                throw new BadRequestException("Invalid Request!");

            assignment.SendNotification = updateAssignment.SendNotification;
            await _assignmentRepository.Update(id, assignment);

            var assignmentDto = _mapper.Map<AssignmentDto>(assignment);

            return assignmentDto;
        }

        public async Task<bool> UpdateCompletionDate(List<Assignment> assignments)
        {
            foreach (var assignment in assignments)
            {
                assignment.LatestCompletion = DateTime.Now;
                assignment.NextCompletion = DateTime.Now.AddDays(assignment.FrequencyInDays);
                try
                {
                    await _assignmentRepository.Update(assignment.Id, assignment);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Latest Completion date update not successful.", ex.Message);
                    return false;
                }
            }

            return true;
        }
    }
}