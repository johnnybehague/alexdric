using Alexdric.Sample.Application.Queries.WeatherForecasts.GetAllWeatherForecast;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Alexdric.Sample.API.Controllers;

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

    //[HttpGet("GetById")]
    //public async Task<IActionResult> GetByIdAsync([FromQuery] string id)
    //{
    //    var response = await _mediator.Send(new GetByIdWeatherForecastQuery() { Id = id });
    //    if (response.Succcess)
    //    {
    //        return Ok(response);
    //    }

    //    return BadRequest(response);
    //}

    //[HttpGet("Count")]
    //public async Task<IActionResult> CountAsync()
    //{
    //    var response = await _mediator.Send(new CountWeatherForecastQuery());
    //    if (response.Succcess)
    //    {
    //        return Ok(response);
    //    }

    //    return BadRequest(response);
    //}

    //[HttpPost("Create")]
    //public async Task<ActionResult> InsertAsync([FromBody] CreateWeatherForecastCommand command)
    //{
    //    if (command is null) return BadRequest();

    //    var response = await _mediator.Send(command);

    //    if (response.Succcess)
    //    {
    //        return Ok(response);
    //    }

    //    return BadRequest(response);
    //}

    //[HttpPut("Update")]
    //public async Task<ActionResult> UpdateAsync([FromBody] UpdateWeatherForecastCommand command)
    //{
    //    if (command is null) return BadRequest();

    //    var response = await _mediator.Send(command);

    //    if (response.Succcess)
    //    {
    //        return Ok(response);
    //    }

    //    return BadRequest(response);
    //}

    //[HttpPut("Delete")]
    //public async Task<ActionResult> DeleteAsync([FromQuery] DeleteWeatherForecastCommand command)
    //{
    //    if (command is null) return BadRequest();

    //    var response = await _mediator.Send(command);

    //    if (response.Succcess)
    //    {
    //        return Ok(response);
    //    }

    //    return BadRequest(response);
    //}
}