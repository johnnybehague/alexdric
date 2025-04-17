using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Alexdric.Application.Behaviours;

public static class BaseConfigureBehaviours
{
    public static void AddBehaviours(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
    }
}
