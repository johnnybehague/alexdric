using Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;
using Alexdric.Sample.Application.DTOs;
using FluentValidation.TestHelper;

namespace Alexdric.Sample.Tests.Application.Commands.CreateWeatherForecast;

/// <summary>
/// Tests of the class CreateWeatherForecastCommandValidator
/// </summary>
[TestClass]
public class CreateWeatherForecastCommandValidatorTests
{
    private CreateWeatherForecastCommandValidator _validator;

    /// <summary>
    /// Setup differents mocks of the Test class
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        _validator = new CreateWeatherForecastCommandValidator();
    }

    /// <summary>
    /// The validator should have error when Id is empty
    /// </summary>
    [TestMethod]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var command = new CreateWeatherForecastCommand
        {
            Dto = new WeatherForecastDto
            {
            }
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.Id);
    }

    /// <summary>
    /// The validator should have error when Date is incorrect
    /// </summary>
    [TestMethod]
    public void Should_Have_Error_When_Date_Is_Empty()
    {
        var command = new CreateWeatherForecastCommand
        {
            Dto = new WeatherForecastDto
            {
                Id = 1
            }
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Dto.Date);
    }

    /// <summary>
    /// The validator should not have error when the model is valid
    /// </summary>
    [TestMethod]
    public void Should_Not_Have_Error_When_Model_Is_Valid()
    {
        var command = new CreateWeatherForecastCommand
        {
            Dto = new WeatherForecastDto
            {
                Id = 1,
                Date = new DateOnly(2025, 4, 18),
                TemperatureC = 32,
                Summary = "Test"
            }
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
