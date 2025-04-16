using System.ComponentModel;
using System.Globalization;
using Alexdric.Application.Mappers;
using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Domain.Entities;
using AutoMapper;

namespace Alexdric.Sample.Application.Mappings;

public class WeatherForecastMapper : BaseMapper<WeatherForecastEntity, WeatherForecastDto>
{
    public WeatherForecastMapper() : base()
    {
        CreateMap<WeatherForecastEntity, WeatherForecastDto>()
            .ForMember(dto => dto.TemperatureC, entity => entity.MapFrom(x => x.Temperature))
            .ForMember(dto => dto.Date, opt => opt.MapFrom(x => MapDateOnly(x.Date)));

        CreateMap<WeatherForecastDto, WeatherForecastEntity>()
            .ForMember(entity => entity.Temperature, dto => dto.MapFrom(x => x.TemperatureC))
            .ForMember(entity => entity.Date, dto => dto.MapFrom(x => x.Date.ToString(new CultureInfo("fr-FR"))));
    }

    public DateOnly MapDateOnly(string date) // A déplacer dans une autre classe
    {
        return DateOnly.TryParse(date, new CultureInfo("fr-FR"), DateTimeStyles.None, out DateOnly parsedDate) ? parsedDate: DateOnly.MinValue;
    }
}
