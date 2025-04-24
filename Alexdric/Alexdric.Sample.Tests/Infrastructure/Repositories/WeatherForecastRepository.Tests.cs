using Alexdric.Sample.Application.Contexts;
using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Alexdric.Sample.Tests.Infrastructure.Repositories;

[TestClass]
public class WeatherForecastRepositoryTests
{
    private Mock<DbSet<WeatherForecastEntity>> _mockDbSet = null!;
    private Mock<IAppDbContext> _contextMock = null!;
    private WeatherForecastRepository _repository = null!;

    public static Mock<DbSet<T>> CreateMockDbSet<T>(IEnumerable<T> elements) where T : class // A déplacer dans une autre classe de test plus tard
    {
        var data = elements.AsQueryable();
        var asyncData = new TestAsyncEnumerable<T>(data);

        var mockSet = new Mock<DbSet<T>>();

        mockSet.As<IAsyncEnumerable<T>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));

        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(asyncData.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        return mockSet;
    }

    [TestInitialize]
    public void Setup()
    {
        _mockDbSet = new Mock<DbSet<WeatherForecastEntity>>();
        _contextMock = new Mock<IAppDbContext>();
        _repository = new WeatherForecastRepository(_contextMock.Object);
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

    #region GetAllWeatherForecastAsync Tests

    [TestMethod]
    public async Task GetAllWeatherForecastAsync_ReturnsData()
    {
        // Arrange
        var data = new List<WeatherForecastEntity>
        {
                new WeatherForecastEntity { Id = 1, Date = "16/04/2025", Temperature = 22, Summary = "Sunny" },
                new WeatherForecastEntity { Id = 2, Date = "17/04/2025", Temperature = 18, Summary = "Cloudy" }
        };
        var mockDbSet = CreateMockDbSet(data);
        _contextMock.Setup(c => c.WeatherForecasts).Returns(mockDbSet.Object);

        // Act
        var result = await _repository.GetAllWeatherForecastAsync();

        // Assert
        Assert.IsNotNull(result);
        var list = result.ToList();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("Sunny", list[0].Summary);
        _contextMock.Verify(c => c.WeatherForecasts, Times.Once);
    }

    [TestMethod]
    public async Task GetAllWeatherForecastAsync_ShouldReturnEmptyList_WhenNoData()
    {
        // Arrange
        var data = Enumerable.Empty<WeatherForecastEntity>();
        var mockDbSet = CreateMockDbSet(data);
        _contextMock.Setup(c => c.WeatherForecasts).Returns(mockDbSet.Object);

        // Act
        var result = await _repository.GetAllWeatherForecastAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());
    }

    #endregion

    #region GetByIdWeatherForecastAsync Tests

    [TestMethod]
    public async Task GetByIdWeatherForecastAsync_ShouldReturnEntity_WhenFound()
    {
        // Arrange
        var weatherForecast = new WeatherForecastEntity { Id = 1, Summary = "Sunny", Temperature = 25 };
        var data = new List<WeatherForecastEntity> { weatherForecast };
        var mockDbSet = CreateMockDbSet(data);
        _contextMock.Setup(c => c.WeatherForecasts).Returns(mockDbSet.Object);

        // Act
        var result = await _repository.GetByIdWeatherForecastAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(weatherForecast.Id, result.Id);
        Assert.AreEqual(weatherForecast.Summary, result.Summary);
    }

    [TestMethod]
    public async Task GetByIdWeatherForecastAsync_ShouldThrowException_WhenNotFound()
    {
        // Arrange
        var data = Enumerable.Empty<WeatherForecastEntity>();
        var mockDbSet = CreateMockDbSet(data);
        _contextMock.Setup(c => c.WeatherForecasts).Returns(mockDbSet.Object);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await _repository.GetByIdWeatherForecastAsync(1));
    }

    #endregion

    #region CreateAsync Tests

    [TestMethod]
    public async Task CreateAsync_ShouldReturnAdded_WhenSaveChangesReturnsPositive()
    {
        // Arrange
        var entity = new WeatherForecastEntity() { Id = 1};
        var data = Enumerable.Empty<WeatherForecastEntity>();
        var mockDbSet = CreateMockDbSet(data);
        _contextMock.Setup(c => c.WeatherForecasts).Returns(mockDbSet.Object);

        mockDbSet.Setup(d => d.AddAsync(entity, It.IsAny<CancellationToken>()))
                  .ReturnsAsync((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<WeatherForecastEntity>)null); // Not used

        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _repository.CreateAsync(entity);

        // Assert
        Assert.AreEqual(EntityState.Added, result);
        mockDbSet.Verify(d => d.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task CreateAsync_ShouldReturnUnchanged_WhenSaveChangesReturnsZero()
    {
        // Arrange
        var entity = new WeatherForecastEntity() { Id = 1 };
        var data = Enumerable.Empty<WeatherForecastEntity>();
        var mockDbSet = CreateMockDbSet(data);

        _contextMock.Setup(c => c.WeatherForecasts).Returns(mockDbSet.Object);
        mockDbSet.Setup(d => d.AddAsync(entity, It.IsAny<CancellationToken>()))
                  .ReturnsAsync((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<WeatherForecastEntity>)null);

        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

        // Act
        var result = await _repository.CreateAsync(entity);

        // Assert
        Assert.AreEqual(EntityState.Unchanged, result);
    }

    #endregion
}
