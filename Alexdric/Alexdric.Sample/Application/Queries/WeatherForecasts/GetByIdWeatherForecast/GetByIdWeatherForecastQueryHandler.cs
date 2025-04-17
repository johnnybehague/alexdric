using Alexdric.Application.Common;
using Alexdric.Application.Queries;
using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Alexdric.Sample.Application.Queries.WeatherForecasts.GetByIdWeatherForecast;

public class GetByIdWeatherForecastQueryHandler : BaseQueryHandler<WeatherForecastEntity, WeatherForecastDto>,
    IRequestHandler<GetByIdWeatherForecastQuery, BaseResponse<WeatherForecastDto>>
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public GetByIdWeatherForecastQueryHandler(IWeatherForecastRepository weatherForecastRepository, IMapper mapper) : base(mapper)
    {
        _weatherForecastRepository = weatherForecastRepository ?? throw new ArgumentNullException(nameof(weatherForecastRepository));
    }

    public async Task<BaseResponse<WeatherForecastDto>> Handle(GetByIdWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        BaseResponse<WeatherForecastDto> response;

        try
        {
            var entity = await _weatherForecastRepository.GetByIdWeatherForecastAsync(request.Id);
            response = GetSuccessResponse(entity);
        }
        catch (Exception ex)
        {
            response = GetErrorResponse(ex);
        }

        return response;
    }
}
