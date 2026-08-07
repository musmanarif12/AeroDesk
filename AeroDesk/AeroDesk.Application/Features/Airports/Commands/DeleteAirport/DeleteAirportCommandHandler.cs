using AeroDesk.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Airports.Commands.DeleteAirport
{
    public class DeleteAirportCommandHandler
        : IRequestHandler<DeleteAirportCommand, bool>
    {
        private readonly IApplicationDbContext _context;


        public DeleteAirportCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> Handle(
            DeleteAirportCommand request,
            CancellationToken cancellationToken)
        {
            var airport = await _context.Airports
                .FirstOrDefaultAsync(
                    a => a.Id == request.Id,
                    cancellationToken);


            if (airport == null)
            {
                return false;
            }


            _context.Airports.Remove(airport);


            await _context.SaveChangesAsync(cancellationToken);


            return true;
        }
    }
}