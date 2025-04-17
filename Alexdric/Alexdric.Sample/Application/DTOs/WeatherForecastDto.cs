using Alexdric.Application.DTOs;

namespace Alexdric.Sample.Application.DTOs;

public record WeatherForecastDto : IDto
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
