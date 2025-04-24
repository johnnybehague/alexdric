using FluentValidation;

namespace Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;

public class CreateWeatherForecastCommandValidator : AbstractValidator<CreateWeatherForecastCommand>
{
    public CreateWeatherForecastCommandValidator()
    {
        RuleFor(x => x.Dto.Id).NotEmpty().NotNull();
        RuleFor(x => x.Dto.Date).NotEmpty().NotNull();
    }
}
