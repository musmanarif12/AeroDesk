using AeroDesk.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Airlines.Commands.DeleteAirline
{
    public class DeleteAirlineCommandHandler
        : IRequestHandler<DeleteAirlineCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAirlineCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            DeleteAirlineCommand request,
            CancellationToken cancellationToken)
        {
            var airline = await _context.Airlines
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (airline == null)
            {
                return false;
            }

            _context.Airlines.Remove(airline);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}