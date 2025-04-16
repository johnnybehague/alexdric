using Alexdric.Sample.Application.Queries.WeatherForecasts.GetAllWeatherForecast;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alexdric.Sample.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _mediator.Send(new GetAllWeatherForecastQuery());
        if (response.Succcess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}