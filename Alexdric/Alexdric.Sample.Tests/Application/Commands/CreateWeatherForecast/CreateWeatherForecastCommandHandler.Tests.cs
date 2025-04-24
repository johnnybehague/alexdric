using Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;
using Alexdric.Sample.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Alexdric.Sample.Domain.Repositories;
using Alexdric.Application.Commands;

namespace Alexdric.Sample.Tests.Application.Commands.CreateWeatherForecast;

[TestClass]
public class CreateWeatherForecastCommandHandlerTests 
{
    private Mock<IWeatherForecastRepository> _repositoryMock;
    private Mock<IMapper> _mapperMock;
    private CreateWeatherForecastCommandHandler _handler;

    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<IWeatherForecastRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateWeatherForecastCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Success_When_WeatherForecast_Created_Successfully()
    {
        // Arrange
        var command = new BaseCreateCommand<WeatherForecastEntity>
        {
            Entity = new WeatherForecastEntity
            {
                Id = 1
            }
        };

        // Mock du mapper pour transformer la commande en entité
        _mapperMock.Setup(m => m.Map<WeatherForecastEntity>(command)).Returns(command.Entity);

        // Mock de la méthode CreateAsync du repository
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<WeatherForecastEntity>())).ReturnsAsync(EntityState.Added);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Succcess);
        Assert.AreEqual("Create succeed!", result.Message);
        Assert.IsTrue(result.Data);

        _mapperMock.Verify(m => m.Map<WeatherForecastEntity>(command), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<WeatherForecastEntity>()), Times.Once);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Failure_When_WeatherForecast_Creation_Fails()
    {
        // Arrange
        var command = new BaseCreateCommand<WeatherForecastEntity>
        {
            Entity = new WeatherForecastEntity
            {
                Id = 1,
                Date = "2025-04-18",
                Temperature = 20,
                Summary = "Sunny"
            }
        };

        // Mock du mapper pour transformer la commande en entité
        _mapperMock.Setup(m => m.Map<WeatherForecastEntity>(command)).Returns(command.Entity);

        // Mock de la méthode CreateAsync du repository
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<WeatherForecastEntity>())).ReturnsAsync(EntityState.Detached); // Simulation de l'échec

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Succcess);
        // Assert.AreEqual("Create failed.", result.Message);
        Assert.IsFalse(result.Data);

        _mapperMock.Verify(m => m.Map<WeatherForecastEntity>(command), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<WeatherForecastEntity>()), Times.Once);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Failure_When_Exception_Is_Thrown()
    {
        // Arrange
        var command = new BaseCreateCommand<WeatherForecastEntity>
        {
            Entity = new WeatherForecastEntity
            {
                Id = 1,
                Date = "2025-04-18",
                Temperature = 20,
                Summary = "Sunny"
            }
        };

        // Mock du mapper pour transformer la commande en entité
        _mapperMock.Setup(m => m.Map<WeatherForecastEntity>(command)).Returns(command.Entity);

        // Mock de la méthode CreateAsync du repository pour lancer une exception
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<WeatherForecastEntity>())).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Succcess);
        Assert.AreEqual("Database error", result.Message);
        Assert.IsFalse(result.Data);

        _mapperMock.Verify(m => m.Map<WeatherForecastEntity>(command), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<WeatherForecastEntity>()), Times.Once);
    }

}