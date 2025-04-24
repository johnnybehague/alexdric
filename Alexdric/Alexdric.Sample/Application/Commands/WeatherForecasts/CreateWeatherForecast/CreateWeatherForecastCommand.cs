using Alexdric.Application.Commands;
using Alexdric.Application.Common;
using Alexdric.Sample.Application.DTOs;
using MediatR;

namespace Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;

/// <summary>
/// Command to Create a WeatherForecast
/// </summary>
public class CreateWeatherForecastCommand : 
    BaseCreateCommand<WeatherForecastDto>, 
    IRequest<BaseResponse<bool>>
{

}

