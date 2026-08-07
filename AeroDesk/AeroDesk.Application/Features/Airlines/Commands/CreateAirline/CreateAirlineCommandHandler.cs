using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Airlines.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AeroDesk.Application.Features.Airlines.Commands.CreateAirline
{
    public class CreateAirlineCommandHandler
        : IRequestHandler<CreateAirlineCommand, AirlineDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAirlineCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AirlineDto> Handle(
            CreateAirlineCommand request,
            CancellationToken cancellationToken)
        {
            var airline = _mapper.Map<Airline>(request);

            _context.Airlines.Add(airline);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AirlineDto>(airline);
        }
    }
}