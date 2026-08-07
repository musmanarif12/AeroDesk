using FluentValidation;

namespace AeroDesk.Application.Features.Gates.Commands.UpdateGate
{
    public class UpdateGateCommandValidator
        : AbstractValidator<UpdateGateCommand>
    {
        public UpdateGateCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

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