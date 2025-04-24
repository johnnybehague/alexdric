using Alexdric.Application.Commands;
using Alexdric.Application.Common;
using Alexdric.Domain.Entities;
using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Domain.Repositories;
using Alexdric.Sample.Infrastructure.Repositories;
using AutoMapper;
using MediatR;

namespace Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;

public class CreateWeatherForecastCommandHandler : BaseCreateCommandHandler<WeatherForecastEntity, IWeatherForecastRepository>
    , IRequestHandler<CreateWeatherForecastCommand, BaseResponse<bool>>
{
    public CreateWeatherForecastCommandHandler(IWeatherForecastRepository weatherForecastRepository, IMapper mapper)
        : base(weatherForecastRepository, mapper)
    {

    }

    public Task<BaseResponse<bool>> Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        return this.Handle((BaseCreateCommand<WeatherForecastEntity>)request, cancellationToken);
    }
}
