using Alexdric.Application.Common;
using Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;
using Alexdric.Sample.Application.DTOs;
using MediatR;

namespace Alexdric.Sample.Tests.Application.Commands.CreateWeatherForecast;

/// <summary>
/// Tests of the class CreateWeatherForecastCommand
/// </summary>
[TestClass]
public class CreateWeatherForecastCommandTests 
{
    /// <summary>
    /// The command should store and return the Id correctly
    /// </summary>
    [TestMethod]
    public void Command_ShouldStoreAndReturnIdCorrectly()
    {
        // Arrange
        var command = new CreateWeatherForecastCommand { Dto = new WeatherForecastDto { Id = 1 } };

        // Act & Assert
        Assert.AreEqual(1, command.Dto.Id);
    }

    /// <summary>
    /// The command should implement the IRequest interface
    /// </summary>
    [TestMethod]
    public void Command_ShouldImplementIRequest()
    {
        // Arrange
        var command = new CreateWeatherForecastCommand { Dto = new WeatherForecastDto { Id = 1 } };

        // Act & Assert
        Assert.IsInstanceOfType(command, typeof(IRequest<BaseResponse<bool>>));
    }
}

