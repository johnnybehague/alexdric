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

    [HttpPut]
    public async Task<IActionResult> CreateAsync(WeatherForecastEntity entity)
    {
        try
        {
            var command = new CreateWeatherForecastCommand() { Entity = entity };
            var response = await _mediator.Send(command);
            if (response.Succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}