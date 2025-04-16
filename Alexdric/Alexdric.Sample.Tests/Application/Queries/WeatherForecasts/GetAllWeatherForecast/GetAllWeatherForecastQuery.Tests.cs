using Alexdric.Application.Common;
using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Application.Queries.WeatherForecasts.GetAllWeatherForecast;
using Alexdric.Sample.Domain.Repositories;
using AutoMapper;
using MediatR;
using Moq;

namespace Alexdric.Sample.Tests.Application.Queries.WeatherForecasts.GetAllWeatherForecast;

[TestClass]
public class GetAllWeatherForecastQueryTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<IWeatherForecastRepository> _repositoryMock;
    private Mock<IMapper> _mapperMock;

    private GetAllWeatherForecastQueryHandler _handler;


    [TestInitialize]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _repositoryMock = new Mock<IWeatherForecastRepository>();
        _mapperMock = new Mock<IMapper>();

        _handler = new GetAllWeatherForecastQueryHandler(_repositoryMock.Object, _mapperMock.Object); // Si GetAllWeatherForecastQueryHandler est ton handler
    }

    [TestMethod]
    public async Task GetAllWeatherForecastQuery_ShouldReturnSuccessResponse_WithData()
    {
        // Arrange
        var query = new GetAllWeatherForecastQuery();

        var expectedWeatherForecasts = new List<WeatherForecastDto>
            {
                new WeatherForecastDto { Id = 1, TemperatureC = 25, Date = new DateOnly(2025, 4, 16), Summary = "Sunny" },
                new WeatherForecastDto { Id = 2, TemperatureC = 18, Date = new DateOnly(2025, 4, 17), Summary = "Cloudy" }
            };
        var expectedResponse = new BaseResponse<IEnumerable<WeatherForecastDto>>()
        {
            Succcess = true,
            Data = expectedWeatherForecasts,
            Message = "Query succeeded!"
        };

        // Simule la réponse du gestionnaire de requêtes
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllWeatherForecastQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _mediatorMock.Object.Send(query);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Succcess);
        Assert.AreEqual("Query succeeded!", result.Message);
        Assert.AreEqual(2, result.Data.Count());  // Vérifie qu'il y a deux éléments
    }

    [TestMethod]
    public async Task GetAllWeatherForecastQuery_ShouldReturnErrorResponse_WhenNoData()
    {
        // Arrange
        var query = new GetAllWeatherForecastQuery();
        var expectedResponse = new BaseResponse<IEnumerable<WeatherForecastDto>>()
        {
            Succcess = false,
            Message = "No data found.",
            Errors = new List<BaseError> { new BaseError { ErrorMessage = "No weather data found." } }
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllWeatherForecastQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _mediatorMock.Object.Send(query);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Succcess);
        Assert.AreEqual("No data found.", result.Message);
        Assert.AreEqual(1, result.Errors.Count());  // Vérifie qu'il y a une erreur
    }
}
