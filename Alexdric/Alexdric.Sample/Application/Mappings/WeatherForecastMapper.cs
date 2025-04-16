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
            .ForMember(dto => dto.Date, entity => entity.MapFrom(x => DateOnly.Parse(x.Date, new CultureInfo("fr-FR"))));

        CreateMap<WeatherForecastDto, WeatherForecastEntity>()
            .ForMember(entity => entity.Temperature, dto => dto.MapFrom(x => x.TemperatureC))
            .ForMember(entity => entity.Date, dto => dto.MapFrom(x => x.Date.ToString(new CultureInfo("fr-FR"))));
    }
}
