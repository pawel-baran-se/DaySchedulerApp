using FluentValidation;

namespace DaySchedulerApp.Application.DTOs.Assignment.Validators
{
    public class UpdateAssignmentValidator : AbstractValidator<UpdateAssignmentDto>
    {
        public UpdateAssignmentValidator()
        {
            RuleFor(a => a.FrequencyInDays)
                .GreaterThanOrEqualTo(1).WithMessage("Maximum once a day!")
                .LessThanOrEqualTo(365).WithMessage("Minimum once a year!")
                .NotNull();

            RuleFor(a => a.SendNotification)
                .NotNull();
        }
    }
}