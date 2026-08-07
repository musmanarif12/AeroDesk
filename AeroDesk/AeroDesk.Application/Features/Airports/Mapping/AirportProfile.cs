using AeroDesk.Application.Features.Airports.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;

namespace AeroDesk.Application.Features.Airports.Mapping
{
    public class AirportProfile : Profile
    {
        public AirportProfile()
        {
            CreateMap<Airport, AirportDto>();

            CreateMap<AirportDto, Airport>();
        }
    }
}