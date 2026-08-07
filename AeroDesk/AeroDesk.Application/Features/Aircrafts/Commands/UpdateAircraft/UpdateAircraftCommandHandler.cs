using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Aircrafts.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Aircrafts.Commands.UpdateAircraft
{
    public class UpdateAircraftCommandHandler
        : IRequestHandler<UpdateAircraftCommand, AircraftDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;


        public UpdateAircraftCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<AircraftDto?> Handle(
            UpdateAircraftCommand request,
            CancellationToken cancellationToken)
        {

            var aircraft = await _context.Aircrafts
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);


            if (aircraft == null)
                return null;


            _mapper.Map(request, aircraft);


            aircraft.UpdatedAt = DateTime.UtcNow;


            await _context.SaveChangesAsync(cancellationToken);


            return _mapper.Map<AircraftDto>(aircraft);
        }
    }
}