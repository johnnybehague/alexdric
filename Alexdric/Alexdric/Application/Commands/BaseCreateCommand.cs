using Alexdric.Application.Common;
using Alexdric.Application.DTOs;
using MediatR;

namespace Alexdric.Application.Commands;

public class BaseCreateCommand<TDto> : IRequest<BaseResponse<bool>>
    where TDto : IDto
{
    public required TDto Dto { get; set; }
}
