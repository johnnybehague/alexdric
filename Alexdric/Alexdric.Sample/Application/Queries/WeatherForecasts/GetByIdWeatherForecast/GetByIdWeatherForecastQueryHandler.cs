using Alexdric.Application.Common;
using Alexdric.Application.Queries;
using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Alexdric.Sample.Application.Queries.WeatherForecasts.GetByIdWeatherForecast;

public class GetByIdWeatherForecastQueryHandler : BaseGetByIdQueryHandler<WeatherForecastEntity, WeatherForecastDto>,
    IRequestHandler<GetByIdWeatherForecastQuery, BaseResponse<WeatherForecastDto>>
{
    public GetByIdWeatherForecastQueryHandler(IWeatherForecastRepository weatherForecastRepository, IMapper mapper) : base(weatherForecastRepository, mapper) { }

    public async Task<BaseResponse<WeatherForecastDto>> Handle(GetByIdWeatherForecastQuery request, CancellationToken cancellationToken)
        => await base.Handle(request, cancellationToken);
}
