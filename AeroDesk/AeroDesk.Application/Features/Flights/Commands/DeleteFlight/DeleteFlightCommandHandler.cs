using AeroDesk.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Flights.Commands.DeleteFlight
{
    public class DeleteFlightCommandHandler
        : IRequestHandler<DeleteFlightCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteFlightCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            DeleteFlightCommand request,
            CancellationToken cancellationToken)
        {
            var flight = await _context.Flights
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (flight == null)
            {
                return false;
            }

            _context.Flights.Remove(flight);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}