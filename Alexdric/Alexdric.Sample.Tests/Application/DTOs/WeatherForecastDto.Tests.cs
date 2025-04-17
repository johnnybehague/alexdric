using Alexdric.Application.DTOs;
using Alexdric.Sample.Application.DTOs;

namespace Alexdric.Sample.Tests.Application.DTOs;

[TestClass]
public class WeatherForecastDtoTests
{
    [TestMethod]
    public void WeatherForecastDto_ShouldInitializeWithCorrectValues()
    {
        // Arrange
        var forecast = new WeatherForecastDto
        {
            Id = 1,
            Date = new DateOnly(2025, 4, 16),
            TemperatureC = 20,
            Summary = "Sunny"
        };

        // Act & Assert
        Assert.AreEqual(1, forecast.Id);
        Assert.AreEqual(new DateOnly(2025, 4, 16), forecast.Date);
        Assert.AreEqual(20, forecast.TemperatureC);
        Assert.AreEqual("Sunny", forecast.Summary);
    }

    [TestMethod]
    public void TemperatureF_ShouldReturnCorrectValue()
    {
        // Arrange
        var forecast = new WeatherForecastDto
        {
            TemperatureC = 20
        };

        // Act
        var temperatureF = forecast.TemperatureF;

        // Assert
        Assert.AreEqual(67, temperatureF, "Temperature in Fahrenheit is not correct.");
    }

    [TestMethod]
    public void TemperatureF_ShouldReturnCorrectValue_ForNegativeCelsius()
    {
        // Arrange
        var forecast = new WeatherForecastDto
        {
            TemperatureC = -10
        };

        // Act
        var temperatureF = forecast.TemperatureF;

        // Assert
        Assert.AreEqual(15, temperatureF, "Temperature in Fahrenheit for negative Celsius value is incorrect.");
    }

    [TestMethod]
    public void WeatherForecastDto_ShouldAllowNullSummary()
    {
        // Arrange
        var forecast = new WeatherForecastDto
        {
            TemperatureC = 15,
            Summary = null
        };

        // Act & Assert
        Assert.IsNull(forecast.Summary);
    }

    [TestMethod]
    public void WeatherForecastDto_ShouldImplementIDto()
    {
        // Arrange
        var dto = new WeatherForecastDto();

        // Assert
        Assert.IsInstanceOfType(dto, typeof(IDto));
    }
}