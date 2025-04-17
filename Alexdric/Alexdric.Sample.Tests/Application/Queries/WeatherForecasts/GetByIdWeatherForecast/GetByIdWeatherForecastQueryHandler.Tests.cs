using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Application.Queries.WeatherForecasts.GetByIdWeatherForecast;
using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Domain.Repositories;
using AutoMapper;
using Moq;

namespace Alexdric.Sample.Tests.Application.Queries.WeatherForecasts.GetByIdWeatherForecast;

[TestClass]
public class GetByIdWeatherForecastQueryHandlerTests
{
    private Mock<IWeatherForecastRepository> _repoMock = null!;
    private Mock<IMapper> _mapperMock = null!;
    private GetByIdWeatherForecastQueryHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _repoMock = new Mock<IWeatherForecastRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetByIdWeatherForecastQueryHandler(_repoMock.Object, _mapperMock.Object);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnSuccessResponse_WhenEntityIsFound()
    {
        // Arrange
        var query = new GetByIdWeatherForecastQuery { Id = 1 };
        var entity = new WeatherForecastEntity { Id = 1, Summary = "Sunny" };
        var dto = new WeatherForecastDto { Id = 1, Summary = "Sunny" };

        _repoMock
            .Setup(r => r.GetByIdWeatherForecastAsync(query.Id))
            .ReturnsAsync(entity);

        _mapperMock
            .Setup(m => m.Map<WeatherForecastDto>(entity))
            .Returns(dto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Succcess);
        Assert.AreEqual(dto, result.Data);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnErrorResponse_WhenExceptionIsThrown()
    {
        // Arrange
        var query = new GetByIdWeatherForecastQuery { Id = 99 };

        _repoMock
            .Setup(r => r.GetByIdWeatherForecastAsync(query.Id))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Succcess);
        Assert.IsTrue(result.Message?.Contains("Test exception") ?? false);
    }
}
