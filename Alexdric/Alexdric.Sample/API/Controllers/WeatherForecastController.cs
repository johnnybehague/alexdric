using Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;
using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Application.Queries.WeatherForecasts.GetAllWeatherForecast;
using Alexdric.Sample.Application.Queries.WeatherForecasts.GetByIdWeatherForecast;
using Alexdric.Sample.Domain.Entities;
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

    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var response = await _mediator.Send(new GetByIdWeatherForecastQuery() { Id = id });
        if (response.Succcess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// PUT : Create a new WeatherForecast
    /// </summary>
    /// <param name="dto">DTO to create</param>
    /// <returns>ActionResult of the result</returns>
    [HttpPut]
    public async Task<IActionResult> CreateAsync(WeatherForecastDto dto)
    {
        var command = new CreateWeatherForecastCommand() { Dto = dto };
        var response = await _mediator.Send(command);
        if (response.Succcess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}