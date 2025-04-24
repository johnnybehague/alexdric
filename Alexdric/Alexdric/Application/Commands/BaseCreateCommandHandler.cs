using Alexdric.Application.Common;
using Alexdric.Domain.Entities;
using Alexdric.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alexdric.Application.Commands;

public class BaseCreateCommandHandler<TEntity, TRepository> // : IRequestHandler<BaseCreateCommand<TEntity>, BaseResponse<bool>>
    where TEntity : IEntity
    where TRepository : ICreateRepository<TEntity>
{
    private readonly IMapper _mapper;
    private readonly TRepository _repository;

    public BaseCreateCommandHandler(TRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseResponse<bool>> Handle(BaseCreateCommand<TEntity> command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var entity = _mapper.Map<TEntity>(command.Entity);
            response.Data = await CreateAsync(entity);

            if (response.Data)
            {
                response.Succcess = true;
                response.Message = "Create succeed!";
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }
        return response;
    }

    private async Task<bool> CreateAsync(TEntity entity)
    {
        var result = await _repository.CreateAsync(entity);
        return result == EntityState.Added;
    }
}
