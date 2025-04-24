using Alexdric.Application.Common;
using Alexdric.Application.DTOs;
using Alexdric.Domain.Entities;
using Alexdric.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alexdric.Application.Commands;

public class BaseCreateCommandHandler<TEntity, TDto> // : IRequestHandler<BaseCreateCommand<TEntity>, BaseResponse<bool>>
    where TEntity : IEntity
    where TDto : IDto
{
    private readonly IMapper _mapper;
    private readonly ICreateRepository<TEntity> _repository;

    public BaseCreateCommandHandler(ICreateRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseResponse<bool>> Handle(BaseCreateCommand<TDto> command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        try
        {
            var entity = _mapper.Map<TEntity>(command.Dto);
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
