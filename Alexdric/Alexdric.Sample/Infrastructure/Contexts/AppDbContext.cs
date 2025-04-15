using Alexdric.Sample.Application.Contexts;
using Alexdric.Sample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alexdric.Sample.Infrastructure.Contexts;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<WeatherForecastEntity> WeatherForecasts { get; set; }
}

