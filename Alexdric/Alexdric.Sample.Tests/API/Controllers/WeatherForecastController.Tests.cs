using Alexdric.Application.Common;
using Alexdric.Sample.API.Controllers;
using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Application.Queries.WeatherForecasts.GetAllWeatherForecast;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Alexdric.Sample.Tests.API.Controllers;

[TestClass]
public class WeatherForecastControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private WeatherForecastController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new WeatherForecastController(_mediatorMock.Object);
    }

    #region Constructor Tests

    [TestMethod]
    public void Constructor_ShouldThrowArgumentNullException_WhenMediatorIsNull()
    {
        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => new WeatherForecastController(null));
    }

    #endregion

    #region GetAllAsync Tests

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnOkResult_WhenSuccessResponse()
    {
        // Arrange
        var successResponse = new BaseResponse<IEnumerable<WeatherForecastDto>>
        {
            Succcess = true,
            Data = new List<WeatherForecastDto> { new WeatherForecastDto { TemperatureC = 20, Summary = "Sunny" } }
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllWeatherForecastQuery>(), default)).ReturnsAsync(successResponse);

        // Act
        var result = await _controller.GetAllAsync();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode); // HTTP 200 OK
        var response = okResult.Value as BaseResponse<IEnumerable<WeatherForecastDto>>;
        Assert.IsTrue(response.Succcess);
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnBadRequest_WhenErrorResponse()
    {
        // Arrange
        var errorResponse = new BaseResponse<IEnumerable<WeatherForecastDto>>
        {
            Succcess = false,
            Message = "Failed to get weather data"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllWeatherForecastQuery>(), default)).ReturnsAsync(errorResponse);

        // Act
        var result = await _controller.GetAllAsync();

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual(400, badRequestResult.StatusCode); // HTTP 400 BadRequest
        var response = badRequestResult.Value as BaseResponse<IEnumerable<WeatherForecastDto>>;
        Assert.IsFalse(response.Succcess);
        Assert.AreEqual("Failed to get weather data", response.Message);
    }

    #endregion
}