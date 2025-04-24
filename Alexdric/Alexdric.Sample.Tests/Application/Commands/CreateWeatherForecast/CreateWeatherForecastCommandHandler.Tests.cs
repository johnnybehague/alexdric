using Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;
using Alexdric.Sample.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Alexdric.Sample.Domain.Repositories;
using Alexdric.Application.Commands;
using Alexdric.Sample.Application.DTOs;

namespace Alexdric.Sample.Tests.Application.Commands.CreateWeatherForecast;

/// <summary>
/// Tests de la classe CreateWeatherForecastCommandHandler
/// </summary>
[TestClass]
public class CreateWeatherForecastCommandHandlerTests 
{
    private Mock<IWeatherForecastRepository> _repositoryMock;
    private Mock<IMapper> _mapperMock;
    private CreateWeatherForecastCommandHandler _handler;

    /// <summary>
    /// Setup differents mocks of the Test class
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<IWeatherForecastRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateWeatherForecastCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    /// <summary>
    /// The handler should return Success when the WeatherForecast is created successfuly
    /// </summary>
    /// <returns>Task</returns>
    [TestMethod]
    public async Task Handle_Should_Return_Success_When_WeatherForecast_Created_Successfully()
    {
        // Arrange
        var command = new BaseCreateCommand<WeatherForecastDto> { Dto = new WeatherForecastDto { Id = 1 }};
        var entity = new WeatherForecastEntity { Id = 1 };

        // Mocks
        _mapperMock.Setup(m => m.Map<WeatherForecastEntity>(command.Dto)).Returns(entity);
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<WeatherForecastEntity>())).ReturnsAsync(EntityState.Added);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Succcess);
        Assert.AreEqual("Create succeed!", result.Message);
        Assert.IsTrue(result.Data);

        _mapperMock.Verify(m => m.Map<WeatherForecastEntity>(command.Dto), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<WeatherForecastEntity>()), Times.Once);
    }

    /// <summary>
    /// The handler should return Failure when the WeatherForecast creation failed
    /// </summary>
    /// <returns>Task</returns>
    [TestMethod]
    public async Task Handle_Should_Return_Failure_When_WeatherForecast_Creation_Fails()
    {
        // Arrange
        var command = new BaseCreateCommand<WeatherForecastDto> { Dto = new WeatherForecastDto { Id = 1, Date = new DateOnly(2025, 4, 18), TemperatureC = 20, Summary = "Sunny" }};
        var entity = new WeatherForecastEntity { Id = 1, Date = "2025-04-18", Temperature = 20, Summary = "Sunny" };


        // Mocks
        _mapperMock.Setup(m => m.Map<WeatherForecastEntity>(command)).Returns(entity);
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<WeatherForecastEntity>())).ReturnsAsync(EntityState.Detached);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Succcess);
        Assert.IsFalse(result.Data);

        _mapperMock.Verify(m => m.Map<WeatherForecastEntity>(command.Dto), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<WeatherForecastEntity>()), Times.Once);
    }

    /// <summary>
    /// The handler should return failure when an exception is thrown
    /// </summary>
    /// <returns>Task</returns>
    [TestMethod]
    public async Task Handle_Should_Return_Failure_When_Exception_Is_Thrown()
    {
        // Arrange
        var command = new BaseCreateCommand<WeatherForecastDto> { Dto = new WeatherForecastDto { Id = 1, Date = new DateOnly(2025, 4, 18), TemperatureC = 20, Summary = "Sunny" } };
        var entity = new WeatherForecastEntity { Id = 1, Date = "2025-04-18", Temperature = 20, Summary = "Sunny" };

        // Mocks
        _mapperMock.Setup(m => m.Map<WeatherForecastEntity>(command.Dto)).Returns(entity);
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<WeatherForecastEntity>())).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Succcess);
        Assert.AreEqual("Database error", result.Message);
        Assert.IsFalse(result.Data);

        _mapperMock.Verify(m => m.Map<WeatherForecastEntity>(command.Dto), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<WeatherForecastEntity>()), Times.Once);
    }
}