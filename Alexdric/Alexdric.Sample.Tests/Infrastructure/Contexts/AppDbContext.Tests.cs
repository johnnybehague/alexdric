using Alexdric.Sample.Domain.Entities;
using Alexdric.Sample.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Alexdric.Sample.Tests.Infrastructure.Contexts;

[TestClass]
public class AppDbContextTests
{
    private DbContextOptions<AppDbContext> _dbContextOptions;

    [TestInitialize]
    public void Setup()
    {
        // Utilise une base de données en mémoire différente pour chaque test
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{System.Guid.NewGuid()}")
            .Options;
    }

    [TestMethod]
    public async Task AddWeatherForecast_ShouldPersistData()
    {
        // Arrange
        var entity = new WeatherForecastEntity
        {
            Id = 1,
            Date = "16/04/2025",
            Temperature = 22,
            Summary = "Sunny"
        };

        // Act
        using (var context = new AppDbContext(_dbContextOptions))
        {
            context.WeatherForecasts.Add(entity);
            await context.SaveChangesAsync();
        }

        // Assert
        using (var context = new AppDbContext(_dbContextOptions))
        {
            var result = await context.WeatherForecasts.FindAsync(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Sunny", result.Summary);
        }
    }

    [TestMethod]
    public async Task WeatherForecasts_DbSet_ShouldBeAccessible()
    {
        // Arrange & Act
        using (var context = new AppDbContext(_dbContextOptions))
        {
            var dbSet = context.WeatherForecasts;

            // Assert
            Assert.IsNotNull(dbSet);
            Assert.IsInstanceOfType(dbSet, typeof(DbSet<WeatherForecastEntity>));
        }
    }

    [TestMethod]
    public async Task CanQueryWeatherForecasts()
    {
        // Arrange
        using (var context = new AppDbContext(_dbContextOptions))
        {
            context.WeatherForecasts.Add(new WeatherForecastEntity
            {
                Id = 1,
                Date = "16/04/2025",
                Temperature = 18,
                Summary = "Cloudy"
            });
            await context.SaveChangesAsync();
        }

        // Act
        using (var context = new AppDbContext(_dbContextOptions))
        {
            var forecasts = await context.WeatherForecasts
                .Where(f => f.Summary == "Cloudy")
                .ToListAsync();

            // Assert
            Assert.AreEqual(1, forecasts.Count);
            Assert.AreEqual(18, forecasts[0].Temperature);
        }
    }
}
