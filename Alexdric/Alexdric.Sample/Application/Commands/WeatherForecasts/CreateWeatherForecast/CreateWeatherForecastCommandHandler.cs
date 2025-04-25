using Alexdric.Application.Commands;
using Alexdric.Application.Common;
using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;

/// <summary>
/// Handler to manage the WeatherForecast Creation
/// </summary>
public class CreateWeatherForecastCommandHandler : BaseCreateCommandHandler<WeatherForecastEntity, WeatherForecastDto>
    , IRequestHandler<CreateWeatherForecastCommand, BaseResponse<bool>>
{
    /// <summary>
    /// Initialize a new instance of CreateWeatherForecastCommandHandler 
    /// </summary>
    /// <param name="weatherForecastRepository">Repository of WeatherForecast</param>
    /// <param name="mapper">Mapper</param>
    public CreateWeatherForecastCommandHandler(IWeatherForecastRepository weatherForecastRepository, IMapper mapper) : base(weatherForecastRepository, mapper) { }

    /// <summary>
    /// Handle the Creation of a new WeatherForecast
    /// </summary>
    /// <param name="command">Command to create a WeatherForecast</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Result of the Creation</returns>
    public async Task<BaseResponse<bool>> Handle(CreateWeatherForecastCommand command, CancellationToken cancellationToken)
        => await base.Handle((BaseCreateCommand<WeatherForecastDto>)command, cancellationToken);
}
