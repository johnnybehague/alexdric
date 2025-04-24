using Alexdric.Domain.Entities;

namespace Alexdric.Sample.Domain.Entities;

/// <summary>
/// Weatherforecast Entity
/// </summary>
public record WeatherForecastEntity : IEntity
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Date (format DD/MM/YYYY)
    /// </summary>
    public string Date { get; set; } = DateTime.MinValue.ToString();

    /// <summary>
    /// Temperature (Celsius)
    /// </summary>
    public int Temperature { get; set; }

    /// <summary>
    /// Summary
    /// </summary>
    public string? Summary { get; set; }
}
