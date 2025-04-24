using Alexdric.Application.Common;
using Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;
using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Domain.Entities;
using MediatR;

namespace Alexdric.Sample.Tests.Application.Commands.CreateWeatherForecast;

[TestClass]
public class CreateWeatherForecastCommandTests 
{
    [TestMethod]
    public void Command_ShouldStoreAndReturnIdCorrectly()
    {
        // Arrange
        var command = new CreateWeatherForecastCommand 
        {
            Dto = new WeatherForecastDto
            {
                Id = 1
            }
        };

        // Act & Assert
        Assert.AreEqual(1, command.Dto.Id);
    }

    [TestMethod]
    public void Command_ShouldImplementIRequest()
    {
        // Arrange
        var query = new CreateWeatherForecastCommand
        {
            Dto = new WeatherForecastDto
            {
                Id = 1
            }
        };

        // Act & Assert
        Assert.IsInstanceOfType(query, typeof(IRequest<BaseResponse<bool>>));
    }
}

