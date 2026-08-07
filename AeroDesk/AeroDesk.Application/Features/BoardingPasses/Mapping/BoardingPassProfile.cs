using AeroDesk.Application.Features.BoardingPasses.Commands.CreateBoardingPass;
using AeroDesk.Application.Features.BoardingPasses.Commands.UpdateBoardingPass;
using AeroDesk.Application.Features.BoardingPasses.DTOs;
using AeroDesk.Domain.Entities;
using AutoMapper;

namespace AeroDesk.Application.Features.BoardingPasses.Mapping
{
    public class BoardingPassProfile : Profile
    {
        public BoardingPassProfile()
        {
            CreateMap<CreateBoardingPassCommand, BoardingPass>();

            CreateMap<UpdateBoardingPassCommand, BoardingPass>();

            CreateMap<BoardingPass, BoardingPassDto>();

            CreateMap<BoardingPassDto, BoardingPass>();
        }
    }
}