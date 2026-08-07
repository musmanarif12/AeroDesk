using AeroDesk.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Baggages.Commands.DeleteBaggage
{
    public class DeleteBaggageCommandHandler
        : IRequestHandler<DeleteBaggageCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBaggageCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            DeleteBaggageCommand request,
            CancellationToken cancellationToken)
        {
            var baggage = await _context.Baggages
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (baggage == null)
            {
                return false;
            }

            _context.Baggages.Remove(baggage);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}