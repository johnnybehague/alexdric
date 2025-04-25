using Alexdric.Application.Common;
using Alexdric.Application.Queries;
using Alexdric.Sample.Application.DTOs;
using MediatR;

namespace Alexdric.Sample.Application.Queries.WeatherForecasts.GetAllWeatherForecast;

public record GetAllWeatherForecastQuery : BaseGetAllQuery, IRequest<BaseResponse<IEnumerable<WeatherForecastDto>>>;
