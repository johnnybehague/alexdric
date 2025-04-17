using Alexdric.Application.Common;
using Alexdric.Application.Queries;
using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Alexdric.Sample.Application.Queries.WeatherForecasts.GetAllWeatherForecast;

public class GetAllWeatherForecastQueryHandler : BaseQueryHandler<WeatherForecastEntity, WeatherForecastDto>,
    IRequestHandler<GetAllWeatherForecastQuery, BaseResponse<IEnumerable<WeatherForecastDto>>>
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public GetAllWeatherForecastQueryHandler(IWeatherForecastRepository weatherForecastRepository, IMapper mapper) : base(mapper)
    {
        _weatherForecastRepository = weatherForecastRepository ?? throw new ArgumentNullException(nameof(weatherForecastRepository));
    }

    public async Task<BaseResponse<IEnumerable<WeatherForecastDto>>> Handle(GetAllWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        BaseResponse<IEnumerable<WeatherForecastDto>> response;

        try
        {
            var entities = await _weatherForecastRepository.GetAllWeatherForecastAsync();
            response = GetIEnumerableSuccessResponse(entities);
        }
        catch (Exception ex)
        {
            response = GetIEnumerableErrorResponse(ex);
        }

        return response;
    }
}
