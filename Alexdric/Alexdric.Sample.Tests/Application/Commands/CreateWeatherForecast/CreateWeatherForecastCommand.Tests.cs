using Alexdric.Application.Common;
using Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;
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
            Entity = new WeatherForecastEntity
            {
                Id = 1
            }
        };

        // Act & Assert
        Assert.AreEqual(1, command.Entity.Id);
    }

    [TestMethod]
    public void Command_ShouldImplementIRequest()
    {
        // Arrange
        var query = new CreateWeatherForecastCommand();

        // Act & Assert
        Assert.IsInstanceOfType(query, typeof(IRequest<BaseResponse<bool>>));
    }
}

