using AeroDesk.Application.Features.Aircrafts.Commands.CreateAircraft;
using AeroDesk.Application.Features.Aircrafts.Commands.UpdateAircraft;
using AeroDesk.Application.Features.Aircrafts.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;

namespace AeroDesk.Application.Features.Aircrafts.Mapping
{
    public class AircraftProfile : Profile
    {
        public AircraftProfile()
        {
            CreateMap<CreateAircraftCommand, Aircraft>();

            CreateMap<UpdateAircraftCommand, Aircraft>();

            CreateMap<Aircraft, AircraftDto>();
        }
    }
}