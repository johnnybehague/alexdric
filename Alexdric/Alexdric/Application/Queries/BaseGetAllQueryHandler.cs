using Alexdric.Application.Common;
using Alexdric.Application.DTOs;
using Alexdric.Domain.Entities;
using Alexdric.Domain.Repositories;
using AutoMapper;

namespace Alexdric.Application.Queries;

/// <summary>
/// Handler for the BaseGetAllQuery
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
/// <typeparam name="TDto">DTO</typeparam>
public class BaseGetAllQueryHandler<TEntity, TDto>
    where TEntity : IEntity
    where TDto : IDto
{
    private readonly IQueryRepository<TEntity> _repository;
    private readonly BaseQueryFormatter<TEntity, TDto> _formatter;

    /// <summary>
    /// Initialize a new instance of the class BaseGetAllQueryHandler
    /// </summary>
    /// <param name="repository">Repository of the entity</param>
    /// <param name="mapper">Mapper</param>
    /// <exception cref="ArgumentNullException"></exception>
    public BaseGetAllQueryHandler(IQueryRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));       
        ArgumentNullException.ThrowIfNull(mapper);
        _formatter = new BaseQueryFormatter<TEntity, TDto>(mapper);
    }

    /// <summary>
    /// Handle the recuperation of all entities
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Token</param>
    /// <returns>Entities</returns>
    public async Task<BaseResponse<IEnumerable<TDto>>> Handle(BaseGetAllQuery request, CancellationToken cancellationToken)
    {
        BaseResponse<IEnumerable<TDto>> response;

        try
        {
            var entities = await _repository.GetAllAsync();
            response = _formatter.GetIEnumerableSuccessResponse(entities);
        }
        catch (Exception ex)
        {
            response = _formatter.GetIEnumerableErrorResponse(ex);
        }

        return response;
    }
}
