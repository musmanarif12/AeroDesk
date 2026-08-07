using FluentValidation;

namespace AeroDesk.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandValidator
        : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.BookingReference)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.BookingDate)
                .NotEmpty();

            RuleFor(x => x.SeatNumber)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.TravelClass)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Status)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.PassengerId)
                .GreaterThan(0);

            RuleFor(x => x.FlightId)
                .GreaterThan(0);
        }
    }
}