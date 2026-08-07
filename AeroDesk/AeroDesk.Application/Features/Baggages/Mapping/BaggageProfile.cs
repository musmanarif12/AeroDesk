using AeroDesk.Application.Features.Baggages.Commands.CreateBaggage;
using AeroDesk.Application.Features.Baggages.Commands.UpdateBaggage;
using AeroDesk.Application.Features.Baggages.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;

namespace AeroDesk.Application.Features.Baggages.Mapping
{
    public class BaggageProfile : Profile
    {
        public BaggageProfile()
        {
            CreateMap<CreateBaggageCommand, Baggage>();

            CreateMap<UpdateBaggageCommand, Baggage>();

            CreateMap<Baggage, BaggageDto>();

            CreateMap<BaggageDto, Baggage>();
        }
    }
}