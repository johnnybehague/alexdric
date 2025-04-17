using Alexdric.Application.DTOs;
using Alexdric.Domain.Entities;
using AutoMapper;

namespace Alexdric.Application.Mappers;

public class BaseMapper<TEntity, TDto> : Profile 
    where TEntity : IEntity
    where TDto : IDto
{
    public BaseMapper()
    {
        CreateMap<TEntity, TDto>();
        CreateMap<TDto, TEntity>();
    }
}
