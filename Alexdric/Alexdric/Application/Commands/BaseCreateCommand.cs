using Alexdric.Application.Common;
using Alexdric.Domain.Entities;
using MediatR;

namespace Alexdric.Application.Commands;

public class BaseCreateCommand<TEntity> : IRequest<BaseResponse<bool>>
    where TEntity : IEntity
{
    public TEntity Entity { get; set; }
}
