using FluentValidation;

namespace AeroDesk.Application.Features.BoardingPasses.Commands.UpdateBoardingPass
{
    public class UpdateBoardingPassCommandValidator
        : AbstractValidator<UpdateBoardingPassCommand>
    {
        public UpdateBoardingPassCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

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