using FluentValidation;

namespace AeroDesk.Application.Features.CheckIns.Commands.UpdateCheckIn
{
    public class UpdateCheckInCommandValidator
        : AbstractValidator<UpdateCheckInCommand>
    {
        public UpdateCheckInCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

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