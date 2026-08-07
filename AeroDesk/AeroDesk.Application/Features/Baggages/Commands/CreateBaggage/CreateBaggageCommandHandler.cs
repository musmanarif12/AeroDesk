using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Baggages.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AeroDesk.Application.Features.Baggages.Commands.CreateBaggage
{
    public class CreateBaggageCommandHandler
        : IRequestHandler<CreateBaggageCommand, BaggageDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateBaggageCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BaggageDto> Handle(
            CreateBaggageCommand request,
            CancellationToken cancellationToken)
        {
            var baggage = _mapper.Map<Baggage>(request);

            _context.Baggages.Add(baggage);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BaggageDto>(baggage);
        }
    }
}