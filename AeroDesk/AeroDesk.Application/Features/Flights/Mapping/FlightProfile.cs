using AeroDesk.Application.Features.Flights.Commands.CreateFlight;
using AeroDesk.Application.Features.Flights.Commands.UpdateFlight;
using AeroDesk.Application.Features.Flights.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;

namespace AeroDesk.Application.Features.Flights.Mapping
{
    public class FlightProfile : Profile
    {
        public FlightProfile()
        {
            CreateMap<CreateFlightCommand, Flight>();

            CreateMap<UpdateFlightCommand, Flight>();

            CreateMap<Flight, FlightDto>();

            CreateMap<FlightDto, Flight>();
        }
    }
}