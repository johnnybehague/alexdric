using Alexdric.Application.Common;
using Alexdric.Application.Queries;
using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Alexdric.Sample.Application.Queries.WeatherForecasts.GetAllWeatherForecast;

public class GetAllWeatherForecastQueryHandler : BaseGetAllQueryHandler<WeatherForecastEntity, WeatherForecastDto>,
    IRequestHandler<GetAllWeatherForecastQuery, BaseResponse<IEnumerable<WeatherForecastDto>>>
{
    public GetAllWeatherForecastQueryHandler(IWeatherForecastRepository weatherForecastRepository, IMapper mapper) : base(weatherForecastRepository, mapper) { }

    public async Task<BaseResponse<IEnumerable<WeatherForecastDto>>> Handle(GetAllWeatherForecastQuery request, CancellationToken cancellationToken)
        => await base.Handle(request, cancellationToken);
}
