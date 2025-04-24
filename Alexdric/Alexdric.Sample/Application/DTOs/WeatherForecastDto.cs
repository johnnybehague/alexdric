using System.Text.Json.Serialization;
using Alexdric.Application.DTOs;

namespace Alexdric.Sample.Application.DTOs;

/// <summary>
/// WeatherForecast DTO
/// </summary>
public record WeatherForecastDto : IDto
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonRequired]
    public int Id { get; set; }

    /// <summary>
    /// Date (DateOnly)
    /// </summary>
    [JsonRequired]
    public DateOnly Date { get; set; }

    /// <summary>
    /// Temperature (Celsius)
    /// </summary>
    [JsonRequired]
    public int TemperatureC { get; set; }

    /// <summary>
    /// Temperature (Fahreinheit)
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// Summary
    /// </summary>
    public string? Summary { get; set; }
}
