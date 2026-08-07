using FluentValidation;

namespace AeroDesk.Application.Features.CheckIns.Commands.CreateCheckIn
{
    public class CreateCheckInCommandValidator
        : AbstractValidator<CreateCheckInCommand>
    {
        public CreateCheckInCommandValidator()
        {
            RuleFor(x => x.CheckInTime)
                .NotEmpty();

            RuleFor(x => x.BaggageCount)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Status)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.PassengerId)
                .GreaterThan(0);

            RuleFor(x => x.BookingId)
                .GreaterThan(0);

            RuleFor(x => x.FlightId)
                .GreaterThan(0);
        }
    }
}