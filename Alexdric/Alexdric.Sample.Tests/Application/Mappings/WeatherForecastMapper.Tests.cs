using Alexdric.Sample.Application.DTOs;
using Alexdric.Sample.Application.Mappers;
using Alexdric.Sample.Domain.Entities;
using AutoMapper;
using System.Globalization;

namespace Alexdric.Sample.Tests.Application.Mappers;

[TestClass]
public class WeatherForecastMapperTests
{
    private IMapper _mapper;

    [TestInitialize]
    public void Setup()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WeatherForecastMapper>();  // Utilise ton profil de mappage
        });
        _mapper = configuration.CreateMapper();
    }

    [TestMethod]
    public void ShouldMap_WeatherForecastEntity_To_WeatherForecastDto()
    {
        // Arrange
        var entity = new WeatherForecastEntity
        {
            Temperature = 25,  // Celsius
            Date = "16/04/2025",  // Format date fr-FR
        };

        // Act
        var dto = _mapper.Map<WeatherForecastDto>(entity);

        // Assert
        Assert.AreEqual(entity.Temperature, dto.TemperatureC);  // Vérifie que la température est correctement mappée
        Assert.AreEqual(DateOnly.Parse(entity.Date, new CultureInfo("fr-FR")), dto.Date);  // Vérifie la conversion de la date
    }

    [TestMethod]
    public void ShouldMap_WeatherForecastDto_To_WeatherForecastEntity()
    {
        // Arrange
        var dto = new WeatherForecastDto
        {
            TemperatureC = 25,  // Celsius
            Date = new DateOnly(2025, 4, 16),  // DateOnly (avec le format fr-FR)
        };

        // Act
        var entity = _mapper.Map<WeatherForecastEntity>(dto);

        // Assert
        Assert.AreEqual(dto.TemperatureC, entity.Temperature);  // Vérifie que la température est correctement mappée
        Assert.AreEqual(dto.Date.ToString(new CultureInfo("fr-FR")), entity.Date);  // Vérifie la conversion de la date en string
    }

    [TestMethod]
    public void ShouldMap_WeatherForecastEntity_To_WeatherForecastDto_WithCorrectTemperature()
    {
        // Arrange
        var entity = new WeatherForecastEntity
        {
            Temperature = 30  // Celsius
        };

        // Act
        var dto = _mapper.Map<WeatherForecastDto>(entity);

        // Assert
        Assert.AreEqual(entity.Temperature, dto.TemperatureC);  // Vérifie que la température en Celsius est correcte
        Assert.AreEqual(85, dto.TemperatureF);  // Température convertie en Fahrenheit (30°C = 86°F)
    }

    [TestMethod]
    public void ShouldMap_WeatherForecastDto_To_WeatherForecastEntity_WithCorrectTemperature()
    {
        // Arrange
        var dto = new WeatherForecastDto
        {
            TemperatureC = 30  // Celsius
        };

        // Act
        var entity = _mapper.Map<WeatherForecastEntity>(dto);

        // Assert
        Assert.AreEqual(dto.TemperatureC, entity.Temperature);  // Vérifie que la température en Celsius est correcte
    }

    [TestMethod]
    public void ShouldHandle_EmptyDateCorrectly()
    {
        // Arrange
        var entity = new WeatherForecastEntity
        {
            Temperature = 20,
            Date = ""  // Date vide
        };

        // Act
        var dto = _mapper.Map<WeatherForecastDto>(entity);

        // Assert
        Assert.AreEqual(DateOnly.MinValue, dto.Date);  // La conversion d'une chaîne vide devrait donner la valeur minimale de DateOnly
    }

    #region MapDateOnly Tests

    [TestMethod]
    public void MapDateOnly_ValidFrenchDate_ReturnsParsedDate()
    {
        // Arrange
        var input = "16/04/2025";

        // Act
        var result = WeatherForecastMapper.MapDateOnly(input);

        // Assert
        var expected = new DateOnly(2025, 4, 16);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void MapDateOnly_EmptyString_ReturnsMinValue()
    {
        // Arrange
        var input = "";

        // Act
        var result = WeatherForecastMapper.MapDateOnly(input);

        // Assert
        Assert.AreEqual(DateOnly.MinValue, result);
    }

    [TestMethod]
    public void MapDateOnly_NonFrenchFormat_ReturnsParsedDate() // Même en ISO ça passe correctement
    {
        // Arrange
        var input = "2025-04-16"; // ISO format, not fr-FR

        // Act
        var result = WeatherForecastMapper.MapDateOnly(input);

        // Assert
        Assert.AreEqual(new DateOnly(2025, 4, 16), result);
    }

    [TestMethod]
    public void MapDateOnly_InvalidString_ReturnsMinValue()
    {
        // Arrange
        var input = "invalid-date";

        // Act
        var result = WeatherForecastMapper.MapDateOnly(input);

        // Assert
        Assert.AreEqual(DateOnly.MinValue, result);
    }

    #endregion
}
