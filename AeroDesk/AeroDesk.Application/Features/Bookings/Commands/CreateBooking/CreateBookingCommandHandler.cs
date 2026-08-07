using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Application.Features.Bookings.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AeroDesk.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandHandler
        : IRequestHandler<CreateBookingCommand, BookingDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookingCommandHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(
            CreateBookingCommand request,
            CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(request);

            _context.Bookings.Add(booking);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BookingDto>(booking);
        }
    }
}