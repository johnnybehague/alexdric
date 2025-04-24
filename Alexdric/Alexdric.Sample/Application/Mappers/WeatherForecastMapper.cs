using System.Globalization;
using Alexdric.Application.Mappers;
using Alexdric.Sample.Application.Commands.WeatherForecasts.CreateWeatherForecast;
using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Domain.Entities;

namespace Alexdric.Sample.Application.Mappers;

/// <summary>
/// Mapper for the WeatherForecast entity
/// </summary>
public class WeatherForecastMapper : BaseMapper<WeatherForecastEntity, WeatherForecastDto>
{
    /// <summary>
    /// Initialize a new instance of the class WeatherForecastMapper
    /// </summary>
    public WeatherForecastMapper() : base()
    {
        CreateMap<WeatherForecastEntity, WeatherForecastDto>()
            .ForMember(dto => dto.TemperatureC, entity => entity.MapFrom(x => x.Temperature))
            .ForMember(dto => dto.Date, opt => opt.MapFrom(x => MapDateOnly(x.Date)));

        CreateMap<WeatherForecastDto, WeatherForecastEntity>()
            .ForMember(entity => entity.Temperature, dto => dto.MapFrom(x => x.TemperatureC))
            .ForMember(entity => entity.Date, dto => dto.MapFrom(x => x.Date.ToString(new CultureInfo("fr-FR"))));

        CreateMap<CreateWeatherForecastCommand, WeatherForecastEntity>();
    }

    /// <summary>
    /// Map from string to DateOnly
    /// </summary>
    /// <param name="date">Date (format DD/MM/YYYY)</param>
    /// <returns>Date (format DateOnly)</returns>
    public static DateOnly MapDateOnly(string date)
    {
        return DateOnly.TryParse(date, new CultureInfo("fr-FR"), DateTimeStyles.None, out DateOnly parsedDate) ? parsedDate: DateOnly.MinValue;
    }
}
