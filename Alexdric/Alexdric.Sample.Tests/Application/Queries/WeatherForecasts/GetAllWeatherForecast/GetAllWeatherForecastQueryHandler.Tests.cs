using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Application.Queries.WeatherForecasts.GetAllWeatherForecast;
using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Domain.Repositories;
using AutoMapper;
using Moq;

namespace Alexdric.Sample.Tests.Application.Queries.WeatherForecasts.GetAllWeatherForecast;

[TestClass]
public class GetAllWeatherForecastQueryHandlerTests
{
    private Mock<IWeatherForecastRepository> _weatherForecastRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private GetAllWeatherForecastQueryHandler _handler;

    [TestInitialize]
    public void Setup()
    {
        _weatherForecastRepositoryMock = new Mock<IWeatherForecastRepository>();
        _mapperMock = new Mock<IMapper>();

        _handler = new GetAllWeatherForecastQueryHandler(
            _weatherForecastRepositoryMock.Object,
            _mapperMock.Object
        );
    }

    [TestMethod]
    public async Task Handle_ShouldReturnSuccessResponse_WhenDataIsRetrieved()
    {
        // Arrange
        var query = new GetAllWeatherForecastQuery();
        var entities = new List<WeatherForecastEntity>
            {
                new WeatherForecastEntity { Id = 1, Temperature = 20, Date = "16/04/2025" }
            };

        var dtos = new List<WeatherForecastDto>
            {
                new WeatherForecastDto { Id = 1, TemperatureC = 20, Date = new DateOnly(2025, 4, 16) }
            };

        _weatherForecastRepositoryMock
            .Setup(r => r.GetAllWeatherForecastAsync())
            .ReturnsAsync(entities);

        _mapperMock
            .Setup(m => m.Map<IEnumerable<WeatherForecastDto>>(entities))
            .Returns(dtos);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.Succcess);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(1, result.Data.Count());
        Assert.AreEqual("Query succeed!", result.Message);
        _weatherForecastRepositoryMock.Verify(r => r.GetAllWeatherForecastAsync(), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnErrorResponse_WhenRepositoryThrowsException()
    {
        // Arrange
        var query = new GetAllWeatherForecastQuery();
        var exceptionMessage = "Database failure";

        _weatherForecastRepositoryMock
            .Setup(r => r.GetAllWeatherForecastAsync())
            .ThrowsAsync(new System.Exception(exceptionMessage));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.Succcess);
        Assert.AreEqual(exceptionMessage, result.Message);
        Assert.IsNotNull(result.Errors);
        Assert.AreEqual(1, result.Errors.Count());
        _weatherForecastRepositoryMock.Verify(r => r.GetAllWeatherForecastAsync(), Times.Once);
    }
}