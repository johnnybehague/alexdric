using Alexdric.Application.Common;
using Alexdric.Application.DTOs;
using Alexdric.Domain.Entities;
using AutoMapper;
namespace Alexdric.Application.Queries;

public class BaseQueryHandler<TEntity, TDto>
    where TEntity: IEntity
    where TDto: IDto
{
    private readonly IMapper _mapper;

    public BaseQueryHandler(IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public BaseResponse<TDto> GetSuccessResponse(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new BaseResponse<TDto>()
        {
            Data = _mapper.Map<TDto>(entity),
            Succcess = true,
            Message = "Query succeed!",
        };
    }

    public BaseResponse<IEnumerable<TDto>> GetIEnumerableSuccessResponse(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return new BaseResponse<IEnumerable<TDto>>()
        {
            Data = _mapper.Map<IEnumerable<TDto>>(entities),
            Succcess = true,
            Message = "Query succeed!",
        };
    }

    public BaseResponse<TDto> GetErrorResponse(Exception ex)
    {
        var errors = new List<BaseError>
        {
            new BaseError() { ErrorMessage = ex.Message }
        };

        return new BaseResponse<TDto>()
        {
            Succcess = false,
            Message = ex.Message,
            Errors = errors
        };
    }

    public BaseResponse<IEnumerable<TDto>> GetIEnumerableErrorResponse(Exception ex)
    {
        var errors = new List<BaseError>
        {
            new BaseError() { ErrorMessage = ex.Message }
        };

        return new BaseResponse<IEnumerable<TDto>>()
        {
            Succcess = false,
            Message = ex.Message,
            Errors = errors
        };
    }
}
