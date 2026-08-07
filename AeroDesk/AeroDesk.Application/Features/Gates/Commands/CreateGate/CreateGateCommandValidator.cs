using FluentValidation;

namespace AeroDesk.Application.Features.Gates.Commands.CreateGate
{
    public class CreateGateCommandValidator
        : AbstractValidator<CreateGateCommand>
    {
        public CreateGateCommandValidator()
        {
            RuleFor(x => x.GateNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Terminal)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Status)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.AirportId)
                .GreaterThan(0);
        }
    }
}