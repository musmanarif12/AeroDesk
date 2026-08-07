using AeroDesk.Application.Features.CheckIns.Commands.CreateCheckIn;
using AeroDesk.Application.Features.CheckIns.Commands.UpdateCheckIn;
using AeroDesk.Application.Features.CheckIns.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;

namespace AeroDesk.Application.Features.CheckIns.Mapping
{
    public class CheckInProfile : Profile
    {
        public CheckInProfile()
        {
            CreateMap<CreateCheckInCommand, CheckIn>();

            CreateMap<UpdateCheckInCommand, CheckIn>();

            CreateMap<CheckIn, CheckInDto>();

            CreateMap<CheckInDto, CheckIn>();
        }
    }
}