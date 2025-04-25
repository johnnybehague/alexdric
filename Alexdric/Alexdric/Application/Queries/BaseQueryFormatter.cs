using Alexdric.Application.Common;
using Alexdric.Application.DTOs;
using Alexdric.Domain.Entities;
using AutoMapper;

namespace Alexdric.Application.Queries;

internal class BaseQueryFormatter<TEntity, TDto>
    where TEntity : IEntity
    where TDto : IDto
{
    internal readonly IMapper _mapper;

    internal BaseQueryFormatter(IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Get the response formatted with DTOs for entity list
    /// </summary>
    /// <param name="entities">Entities</param>
    /// <returns>BaseResponse with DTOs</returns>
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

    /// <summary>
    /// Get the error response formatted for entity list
    /// </summary>
    /// <param name="ex">Exception to throw</param>
    /// <returns>BaseResponse with Exception</returns>
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

    /// <summary>
    /// Get the response formatted with DTO for Entity
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>BaseResponse with DTO</returns>
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

    /// <summary>
    /// Get the error response formatted for entity
    /// </summary>
    /// <param name="ex">Exception to throw</param>
    /// <returns>BaseResponse with Exception</returns>
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
}
