using AeroDesk.Application.Features.Bookings.Commands.CreateBooking;
using AeroDesk.Application.Features.Bookings.Commands.UpdateBooking;
using AeroDesk.Application.Features.Bookings.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;

namespace AeroDesk.Application.Features.Bookings.Mapping
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<CreateBookingCommand, Booking>();

            CreateMap<UpdateBookingCommand, Booking>();

            CreateMap<Booking, BookingDto>();

            CreateMap<BookingDto, Booking>();
        }
    }
}