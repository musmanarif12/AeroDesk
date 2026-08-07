using FluentValidation;

namespace AeroDesk.Application.Features.Aircrafts.Commands.UpdateAircraft
{
    public class UpdateAircraftCommandValidator
        : AbstractValidator<UpdateAircraftCommand>
    {
        public UpdateAircraftCommandValidator()
        {

            RuleFor(x => x.Id)
                .GreaterThan(0);


            RuleFor(x => x.Name)
                .NotEmpty();


            RuleFor(x => x.Model)
                .NotEmpty();


            RuleFor(x => x.Capacity)
                .GreaterThan(0);


            RuleFor(x => x.AirlineId)
                .GreaterThan(0);
        }
    }
}