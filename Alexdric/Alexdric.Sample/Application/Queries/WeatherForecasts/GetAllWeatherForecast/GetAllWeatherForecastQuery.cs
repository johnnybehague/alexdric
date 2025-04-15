using Alexdric.Application.Common;
using Alexdric.Sample.Application.DTOs;
using MediatR;

namespace Alexdric.Sample.Application.Queries.WeatherForecasts.GetAllWeatherForecast;

public record GetAllWeatherForecastQuery : IRequest<BaseResponse<IEnumerable<WeatherForecastDto>>>;
