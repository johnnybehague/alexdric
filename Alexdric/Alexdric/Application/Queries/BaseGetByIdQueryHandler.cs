using Alexdric.Application.Common;
using Alexdric.Application.DTOs;
using Alexdric.Domain.Entities;
using Alexdric.Domain.Repositories;
using AutoMapper;

namespace Alexdric.Application.Queries;

/// <summary>
/// Handler for the BaseGetByIdQuery
/// </summary>
public class BaseGetByIdQueryHandler<TEntity, TDto>
    where TEntity : IEntity
    where TDto : IDto
{
    private readonly IQueryRepository<TEntity> _repository;
    private readonly BaseQueryFormatter<TEntity, TDto> _formatter;

    /// <summary>
    /// Initialize a new instance of the class BaseGetByIdQueryHandler
    /// </summary>
    /// <param name="repository">Repository of the entity</param>
    /// <param name="mapper">Mapper</param>
    /// <exception cref="ArgumentNullException"></exception>
    public BaseGetByIdQueryHandler(IQueryRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        if (mapper == null)
            throw new ArgumentNullException(nameof(mapper));

        _formatter = new BaseQueryFormatter<TEntity, TDto>(mapper);
    }

    /// <summary>
    /// Handle the recuperation of all entities
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Token</param>
    /// <returns>Entities</returns>
    public async Task<BaseResponse<TDto>> Handle(BaseGetByIdQuery request, CancellationToken cancellationToken)
    {
        BaseResponse<TDto> response;

        try
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            response = _formatter.GetSuccessResponse(entity);
        }
        catch (Exception ex)
        {
            response = _formatter.GetErrorResponse(ex);
        }

        return response;
    }
}
