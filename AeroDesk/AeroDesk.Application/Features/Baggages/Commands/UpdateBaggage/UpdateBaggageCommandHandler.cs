using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Baggages.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Baggages.Commands.UpdateBaggage
{
    public class UpdateBaggageCommandHandler
        : IRequestHandler<UpdateBaggageCommand, BaggageDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBaggageCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BaggageDto?> Handle(
            UpdateBaggageCommand request,
            CancellationToken cancellationToken)
        {
            var baggage = await _context.Baggages
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (baggage == null)
            {
                return null;
            }

            _mapper.Map(request, baggage);

            baggage.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BaggageDto>(baggage);
        }
    }
}