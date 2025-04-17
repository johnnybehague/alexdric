using Alexdric.Application.Common;
using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Application.Queries.WeatherForecasts.GetByIdWeatherForecast;
using MediatR;

namespace Alexdric.Sample.Tests.Application.Queries.WeatherForecasts.GetByIdWeatherForecast;

[TestClass]
public class GetByIdWeatherForecastQueryTests
{
    [TestMethod]
    public void Query_ShouldStoreAndReturnIdCorrectly()
    {
        // Arrange
        var query = new GetByIdWeatherForecastQuery { Id = 42 };

        // Act & Assert
        Assert.AreEqual(42, query.Id);
    }

    [TestMethod]
    public void Queries_WithSameId_ShouldBeEqual()
    {
        // Arrange
        var q1 = new GetByIdWeatherForecastQuery { Id = 5 };
        var q2 = new GetByIdWeatherForecastQuery { Id = 5 };

        // Act & Assert
        Assert.AreEqual(q1, q2);
        Assert.IsTrue(q1 == q2);
    }

    [TestMethod]
    public void Query_ShouldImplementIRequest()
    {
        // Arrange
        var query = new GetByIdWeatherForecastQuery();

        // Act & Assert
        Assert.IsInstanceOfType(query, typeof(IRequest<BaseResponse<WeatherForecastDto>>));
    }
}
