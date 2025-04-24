using System.Text.Json.Serialization;
using Alexdric.Application.DTOs;

namespace Alexdric.Sample.Application.DTOs;

public record WeatherForecastDto : IDto
{
    [JsonRequired]
    public int Id { get; set; }

    [JsonRequired]
    public DateOnly Date { get; set; }

    [JsonRequired]
    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
