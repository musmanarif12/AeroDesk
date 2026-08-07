using AeroDesk.Application.Features.Gates.Commands.CreateGate;
using AeroDesk.Application.Features.Gates.Commands.UpdateGate;
using AeroDesk.Application.Features.Gates.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;

namespace AeroDesk.Application.Features.Gates.Mapping
{
    public class GateProfile : Profile
    {
        public GateProfile()
        {
            CreateMap<CreateGateCommand, Gate>();

            CreateMap<UpdateGateCommand, Gate>();

            CreateMap<Gate, GateDto>();

            CreateMap<GateDto, Gate>();
        }
    }
}