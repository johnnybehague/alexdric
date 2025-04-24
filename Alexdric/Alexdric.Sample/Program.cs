using System.Reflection;
using Alexdric.Sample.Application.Contexts;
using Alexdric.Sample.Domain.Repositories;
using Alexdric.Sample.Infrastructure.Contexts;
using Alexdric.Sample.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Alexdric.Application.Behaviours;
using System.Net;
using MediatR;

namespace Alexdric.Sample;

public class Program
{
    private Program()
    {
    }

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //builder.Services.AddBehaviours();
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IAppDbContext, AppDbContext>();
        builder.Services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}