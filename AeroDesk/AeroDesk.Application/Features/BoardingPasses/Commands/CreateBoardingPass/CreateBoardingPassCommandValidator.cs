using FluentValidation;

namespace AeroDesk.Application.Features.BoardingPasses.Commands.CreateBoardingPass
{
    public class CreateBoardingPassCommandValidator
        : AbstractValidator<CreateBoardingPassCommand>
    {
        public CreateBoardingPassCommandValidator()
        {
            RuleFor(x => x.BoardingPassNumber)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.SeatNumber)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.BoardingTime)
                .NotEmpty();

            RuleFor(x => x.QRCode)
                .NotEmpty();

            RuleFor(x => x.Status)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.CheckInId)
                .GreaterThan(0);
        }
    }
}