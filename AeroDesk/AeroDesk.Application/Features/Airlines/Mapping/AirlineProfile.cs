using AeroDesk.Application.Features.Airlines.Commands.CreateAirline;
using AeroDesk.Application.Features.Airlines.Commands.UpdateAirline;
using AeroDesk.Application.Features.Airlines.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;

namespace AeroDesk.Application.Features.Airlines.Mapping
{
    public class AirlineProfile : Profile
    {
        public AirlineProfile()
        {
            CreateMap<CreateAirlineCommand, Airline>();

            CreateMap<UpdateAirlineCommand, Airline>();

            CreateMap<Airline, AirlineDto>();

            CreateMap<AirlineDto, Airline>();
        }
    }
}