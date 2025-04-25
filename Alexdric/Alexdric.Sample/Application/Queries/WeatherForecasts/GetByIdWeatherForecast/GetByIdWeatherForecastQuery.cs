using Alexdric.Application.Common;
using Alexdric.Application.Queries;
using Alexdric.Sample.Application.DTOs;
using MediatR;

namespace Alexdric.Sample.Application.Queries.WeatherForecasts.GetByIdWeatherForecast;

public record GetByIdWeatherForecastQuery : BaseGetByIdQuery, IRequest<BaseResponse<WeatherForecastDto>> { }
