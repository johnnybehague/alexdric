using Alexdric.Domain.Entities;
using Alexdric.Sample.Domain.Entities;

namespace Alexdric.Sample.Tests.Domain.Entities;

[TestClass]
public class WeatherForecastEntityTests
{
    [TestMethod]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var id = 1;
        var date = "16/04/2025";
        var temperature = 22;
        var summary = "Sunny";

        // Act
        var entity = new WeatherForecastEntity
        {
            Id = id,
            Date = date,
            Temperature = temperature,
            Summary = summary
        };

        // Assert
        Assert.AreEqual(id, entity.Id);
        Assert.AreEqual(date, entity.Date);
        Assert.AreEqual(temperature, entity.Temperature);
        Assert.AreEqual(summary, entity.Summary);
    }

    [TestMethod]
    public void RecordEquality_ShouldWorkCorrectly()
    {
        // Arrange
        var entity1 = new WeatherForecastEntity
        {
            Id = 1,
            Date = "16/04/2025",
            Temperature = 22,
            Summary = "Sunny"
        };

        var entity2 = new WeatherForecastEntity
        {
            Id = 1,
            Date = "16/04/2025",
            Temperature = 22,
            Summary = "Sunny"
        };

        // Act & Assert
        Assert.AreEqual(entity1, entity2);
        Assert.IsTrue(entity1 == entity2);
    }

    [TestMethod]
    public void Properties_ShouldBeMutable()
    {
        // Arrange
        var entity = new WeatherForecastEntity();

        // Act
        entity.Id = 2;
        entity.Date = "17/04/2025";
        entity.Temperature = 18;
        entity.Summary = "Cloudy";

        // Assert
        Assert.AreEqual(2, entity.Id);
        Assert.AreEqual("17/04/2025", entity.Date);
        Assert.AreEqual(18, entity.Temperature);
        Assert.AreEqual("Cloudy", entity.Summary);
    }

    [TestMethod]
    public void InheritsFrom_BaseEntity()
    {
        // Arrange
        var entity = new WeatherForecastEntity();

        // Act & Assert
        Assert.IsInstanceOfType(entity, typeof(BaseEntity));
    }
}
