using Alexdric.Application.Common;
using Alexdric.Application.DTOs;
using MediatR;

namespace Alexdric.Application.Commands;

/// <summary>
/// Command for DTOs Creation
/// </summary>
/// <typeparam name="TDto">DTO to create</typeparam>
public class BaseCreateCommand<TDto> : IRequest<BaseResponse<bool>>
    where TDto : IDto
{
    /// <summary>
    /// DTO
    /// </summary>
    public required TDto Dto { get; set; }
}
