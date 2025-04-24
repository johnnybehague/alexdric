using Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;
using FluentValidation.TestHelper;

namespace Alexdric.Sample.Tests.Application.Commands.CreateWeatherForecast;

[TestClass]
public class CreateWeatherForecastCommandValidatorTests
{
    //private CreateWeatherForecastCommandValidator _validator;

    //[TestInitialize]
    //public void Setup()
    //{
    //    _validator = new CreateWeatherForecastCommandValidator();
    //}

    //[TestMethod]
    //public void Should_Have_Error_When_Id_Is_Empty()
    //{
    //    var model = new CreateWeatherForecastCommand
    //    {
    //        Date = DateOnly.MinValue.ToString(),
    //    };

    //    var result = _validator.TestValidate(model);
    //    result.ShouldHaveValidationErrorFor(x => x.Id);
    //}

    //[TestMethod]
    //public void Should_Have_Error_When_Date_Is_Empty()
    //{
    //    var model = new CreateWeatherForecastCommand
    //    {
    //        Id = 1,
    //        Date = string.Empty
    //    };

    //    var result = _validator.TestValidate(model);
    //    result.ShouldHaveValidationErrorFor(x => x.Date);
    //}

    //[TestMethod]
    //public void Should_Not_Have_Error_When_Model_Is_Valid()
    //{
    //    var model = new CreateWeatherForecastCommand
    //    {
    //        Id = 1,
    //        Date = "18/04/2025",
    //        Temperature = 32,
    //        Summary = "Test"
    //    };

    //    var result = _validator.TestValidate(model);
    //    result.ShouldNotHaveAnyValidationErrors();
    //}
}
