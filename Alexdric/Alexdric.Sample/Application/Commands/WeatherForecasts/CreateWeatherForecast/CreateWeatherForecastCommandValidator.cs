using FluentValidation;

namespace Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;

/// <summary>
/// Validator of the CreateWeatherForecastCommand
/// </summary>
public class CreateWeatherForecastCommandValidator : AbstractValidator<CreateWeatherForecastCommand>
{
    /// <summary>
    /// Initialize a new instance of CreateWeatherForecastCommandValidator
    /// </summary>
    public CreateWeatherForecastCommandValidator()
    {
        RuleFor(x => x.Dto.Id).NotEmpty().NotNull();
        RuleFor(x => x.Dto.Date).NotEmpty().NotNull();
    }
}
