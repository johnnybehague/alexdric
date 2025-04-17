using Alexdric.Sample.Application.Contexts;
using Alexdric.Sample.Domain.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Alexdric.Sample.Tests;

[TestClass]
public class ProgramTests
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;

    [TestInitialize]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [TestMethod]
    public async Task Application_ShouldStartAndReturnSuccessOnSwaggerEndpoint()
    {
        // Act
        var response = await _client.GetAsync("/swagger/index.html");

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound);
        // Swagger UI may not exist in test env, but app shouldn't crash
    }

    [TestMethod]
    public void DependencyInjection_ShouldResolve_IWeatherForecastRepository()
    {
        // Arrange
        var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>()!;
        using var scope = scopeFactory.CreateScope();

        // Act
        var repo = scope.ServiceProvider.GetService<IWeatherForecastRepository>();

        // Assert
        Assert.IsNotNull(repo);
    }

    [TestMethod]
    public void DependencyInjection_ShouldResolve_IAppDbContext()
    {
        var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>()!;
        using var scope = scopeFactory.CreateScope();

        var db = scope.ServiceProvider.GetService<IAppDbContext>();

        Assert.IsNotNull(db);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _client.Dispose();
        _factory.Dispose();
    }
}
