using AeroDesk.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.BoardingPasses.Commands.DeleteBoardingPass
{
    public class DeleteBoardingPassCommandHandler
        : IRequestHandler<DeleteBoardingPassCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBoardingPassCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            DeleteBoardingPassCommand request,
            CancellationToken cancellationToken)
        {
            var boardingPass = await _context.BoardingPasses
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (boardingPass == null)
            {
                return false;
            }

            _context.BoardingPasses.Remove(boardingPass);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}