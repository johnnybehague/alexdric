using Alexdric.Application.DTOs;
using Alexdric.Application.Mappers;
using Alexdric.Domain.Entities;
using AutoMapper;

namespace Alexdric.Tests.Application.Mappers;

[TestClass]
public class BaseMapperTests
{
    #region Test Classes

    public record PersonEntity(string Name, int Age) : BaseEntity;

    public record PersonDto(string Name, int Age) : BaseDto;

    #endregion

    private IMapper _mapper;

    [TestInitialize]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new BaseMapper<PersonEntity, PersonDto>());
        });

        config.AssertConfigurationIsValid(); // Vérifie que les mappings sont valides
        _mapper = config.CreateMapper();
    }

    [TestMethod]
    public void Map_ShouldMapEntityToDto()
    {
        // Arrange
        var entity = new PersonEntity("Alice", 30);

        // Act
        var dto = _mapper.Map<PersonDto>(entity);

        // Assert
        Assert.IsNotNull(dto);
        Assert.AreEqual(entity.Name, dto.Name);
        Assert.AreEqual(entity.Age, dto.Age);
    }

    [TestMethod]
    public void Map_ShouldMapDtoToEntity()
    {
        // Arrange
        var dto = new PersonDto("Bob", 25);

        // Act
        var entity = _mapper.Map<PersonEntity>(dto);

        // Assert
        Assert.IsNotNull(entity);
        Assert.AreEqual(dto.Name, entity.Name);
        Assert.AreEqual(dto.Age, entity.Age);
    }
}
