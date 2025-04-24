using FluentValidation;

namespace Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;

public class CreateWeatherForecastCommandValidator : AbstractValidator<CreateWeatherForecastCommand>
{
    public CreateWeatherForecastCommandValidator()
    {
        RuleFor(x => x.Entity.Id).NotEmpty().NotNull();
        RuleFor(x => x.Entity.Date).NotEmpty().NotNull();
    }
}
