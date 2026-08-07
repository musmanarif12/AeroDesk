using AeroDesk.Application.Features.Passengers.Commands.CreatePassenger;
using AeroDesk.Application.Features.Passengers.Commands.UpdatePassenger;
using AeroDesk.Application.Features.Passengers.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;

namespace AeroDesk.Application.Features.Passengers.Mapping
{
    public class PassengerProfile : Profile
    {
        public PassengerProfile()
        {
            CreateMap<CreatePassengerCommand, Passenger>();

            CreateMap<UpdatePassengerCommand, Passenger>();

            CreateMap<Passenger, PassengerDto>();

            CreateMap<PassengerDto, Passenger>();
        }
    }
}