using Alexdric.Application.Common;
using Alexdric.Sample.Application.DTOs;
using MediatR;

namespace Alexdric.Sample.Application.Queries.WeatherForecasts.GetByIdWeatherForecast;

public record GetByIdWeatherForecastQuery : IRequest<BaseResponse<WeatherForecastDto>>
{
    public int Id { get; set; }
}
