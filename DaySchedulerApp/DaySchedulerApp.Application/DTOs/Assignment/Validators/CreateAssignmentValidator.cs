using FluentValidation;

namespace DaySchedulerApp.Application.DTOs.Assignment.Validators
{
    public class CreateAssignmentValidator : AbstractValidator<CreateAssignmentDto>
    {
        public CreateAssignmentValidator()
        {
            RuleFor(a => a.Name)
                .NotNull()
                .MinimumLength(3).WithMessage("Name must be grater then 3 letters");

            RuleFor(a => a.FrequencyInDays)
                .GreaterThanOrEqualTo(1).WithMessage("Maximum once a day!")
                .LessThanOrEqualTo(365).WithMessage("Minimum once a year!")
                .NotNull();

            RuleFor(a => a.SendNotification)
                .NotNull();
        }
    }
}