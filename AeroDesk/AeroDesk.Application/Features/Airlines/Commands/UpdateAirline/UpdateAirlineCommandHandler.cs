using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Airlines.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Airlines.Commands.UpdateAirline
{
    public class UpdateAirlineCommandHandler
        : IRequestHandler<UpdateAirlineCommand, AirlineDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAirlineCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AirlineDto?> Handle(
            UpdateAirlineCommand request,
            CancellationToken cancellationToken)
        {
            var airline = await _context.Airlines
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (airline == null)
                return null;

            _mapper.Map(request, airline);

            airline.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AirlineDto>(airline);
        }
    }
}