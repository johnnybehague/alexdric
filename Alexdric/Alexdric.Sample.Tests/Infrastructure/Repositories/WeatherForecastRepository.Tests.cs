using Alexdric.Sample.Application.Contexts;
using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Alexdric.Sample.Tests.Infrastructure.Repositories;

[TestClass]
public class WeatherForecastRepositoryTests
{
    private Mock<IAppDbContext> _contextMock = null!;
    private WeatherForecastRepository _repository = null!;

    public static Mock<DbSet<T>> CreateMockDbSet<T>(IEnumerable<T> elements) where T : class // A déplacer dans une autre classe de test plus tard
    {
        var queryable = elements.AsQueryable();

        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

        return mockSet;
    }

    [TestInitialize]
    public void Setup()
    {
        _contextMock = new Mock<IAppDbContext>();
    }

    [TestMethod]
    public void Constructor_NullContext_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            var repo = new WeatherForecastRepository(null!);
        });
    }

    //[TestMethod]
    //public async Task GetAllWeatherForecastAsync_ReturnsData()
    //{
    //    // Arrange
    //    var fakeData = new List<WeatherForecastEntity>
    //        {
    //            new WeatherForecastEntity { Id = 1, Date = "16/04/2025", Temperature = 22, Summary = "Sunny" },
    //            new WeatherForecastEntity { Id = 2, Date = "17/04/2025", Temperature = 18, Summary = "Cloudy" }
    //    };

    //    // var mockDbSet = CreateMockDbSet(fakeData);
    //    var mockDbSet = new Mock<DbSet<WeatherForecastEntity>>();
    //    //mockDbSet.As<IQueryable<WeatherForecastEntity>>().Setup(m => m.Provider).Returns(fakeData.Provider);
    //    //mockDbSet.As<IQueryable<WeatherForecastEntity>>().Setup(m => m.Expression).Returns(fakeData.Expression);
    //    //mockDbSet.As<IQueryable<WeatherForecastEntity>>().Setup(m => m.ElementType).Returns(fakeData.ElementType);
    //    mockDbSet.As<IQueryable<WeatherForecastEntity>>().Setup(m => m.GetEnumerator()).Returns(fakeData.GetEnumerator());

    //    _contextMock.Setup(c => c.WeatherForecasts).Returns(mockDbSet.Object);

    //    _repository = new WeatherForecastRepository(_contextMock.Object);

    //    // Act
    //    var result = await _repository.GetAllWeatherForecastAsync();

    //    // Assert
    //    Assert.IsNotNull(result);
    //    var list = result.ToList();
    //    Assert.AreEqual(2, list.Count);
    //    Assert.AreEqual("Sunny", list[0].Summary);
    //    _contextMock.Verify(c => c.WeatherForecasts, Times.Once);
    //}
}
