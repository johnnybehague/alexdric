using Alexdric.Domain.Entities;

namespace Alexdric.Sample.Domain.Entities;

public record WeatherForecastEntity : IEntity
{
    public int Id { get; set; }

    public string Date { get; set; } = DateTime.MinValue.ToString();

    public int Temperature { get; set; }

    public string? Summary { get; set; }
}
