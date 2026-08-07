using AeroDesk.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Aircrafts.Commands.DeleteAircraft
{
    public class DeleteAircraftCommandHandler
        : IRequestHandler<DeleteAircraftCommand, bool>
    {

        private readonly IApplicationDbContext _context;


        public DeleteAircraftCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> Handle(
            DeleteAircraftCommand request,
            CancellationToken cancellationToken)
        {

            var aircraft = await _context.Aircrafts
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);


            if (aircraft == null)
                return false;


            _context.Aircrafts.Remove(aircraft);


            await _context.SaveChangesAsync(cancellationToken);


            return true;
        }
    }
}