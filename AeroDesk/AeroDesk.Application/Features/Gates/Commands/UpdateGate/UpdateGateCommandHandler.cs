using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Gates.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AeroDesk.Application.Features.Gates.Commands.UpdateGate
{
    public class UpdateGateCommandHandler
        : IRequestHandler<UpdateGateCommand, GateDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGateCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GateDto?> Handle(
            UpdateGateCommand request,
            CancellationToken cancellationToken)
        {
            var gate = await _context.Gates
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (gate == null)
            {
                return null;
            }

            _mapper.Map(request, gate);

            gate.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GateDto>(gate);
        }
    }
}