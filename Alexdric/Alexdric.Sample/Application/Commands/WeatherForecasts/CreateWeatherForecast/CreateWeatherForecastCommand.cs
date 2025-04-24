using Alexdric.Application.Commands;
using Alexdric.Application.Common;
using Alexdric.Sample.Domain.Entities;
using MediatR;

namespace Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;

public class CreateWeatherForecastCommand : 
    BaseCreateCommand<WeatherForecastEntity>, 
    IRequest<BaseResponse<bool>>
{

}

