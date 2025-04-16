using Alexdric.Application.DTOs;
using Alexdric.Domain.Entities;
using AutoMapper;

namespace Alexdric.Application.Mappers;

public class BaseMapper<TEntity, TDto> : Profile 
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    public BaseMapper()
    {
        CreateMap<TEntity, TDto>();
        CreateMap<TDto, TEntity>();
    }
}
