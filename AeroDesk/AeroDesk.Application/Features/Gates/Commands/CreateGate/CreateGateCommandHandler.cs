using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Gates.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AeroDesk.Application.Features.Gates.Commands.CreateGate
{
    public class CreateGateCommandHandler
        : IRequestHandler<CreateGateCommand, GateDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateGateCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GateDto> Handle(
            CreateGateCommand request,
            CancellationToken cancellationToken)
        {
            var gate = _mapper.Map<Gate>(request);

            _context.Gates.Add(gate);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GateDto>(gate);
        }
    }
}