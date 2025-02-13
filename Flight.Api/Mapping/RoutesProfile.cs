using AutoMapper;
using Flight.Domain.Dto;
using Flight.Domain.Entities;

namespace Flight.Api.Mapping;

public class RoutesProfile : Profile
{
    public RoutesProfile()
    {
        CreateMap<RoutesDto, RoutesEntity>()
            .ReverseMap();
    }
}