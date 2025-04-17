using Alexdric.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Alexdric.Tests.Application.Behaviours;

[TestClass]
public class BaseConfigureBehavioursTests
{
    [TestMethod]
    public void AddBehaviours_ShouldRegisterBehavioursCorrectly()
    {
        // Arrange
        var servicesMock = new Mock<IServiceCollection>();
        var serviceDescriptors = new List<ServiceDescriptor>();

        servicesMock
            .Setup(s => s.Add(It.IsAny<ServiceDescriptor>()))
            .Callback<ServiceDescriptor>(descriptor => serviceDescriptors.Add(descriptor));

        // Act
        BaseConfigureBehaviours.AddBehaviours(servicesMock.Object);

        // Assert
        // ValidationBehaviour<,> as Transient
        Assert.IsTrue(serviceDescriptors.Exists(sd =>
            sd.Lifetime == ServiceLifetime.Transient &&
            sd.ServiceType.IsGenericType &&
            sd.ServiceType.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>) &&
            sd.ImplementationType == typeof(ValidationBehaviour<,>)
        ));

        // LoggingBehaviour<,> as Singleton
        Assert.IsTrue(serviceDescriptors.Exists(sd =>
            sd.Lifetime == ServiceLifetime.Singleton &&
            sd.ImplementationType == typeof(LoggingBehaviour<,>)
        ));

        // PerformanceBehaviour<,> as Singleton
        Assert.IsTrue(serviceDescriptors.Exists(sd =>
            sd.Lifetime == ServiceLifetime.Singleton &&
            sd.ImplementationType == typeof(PerformanceBehaviour<,>)
        ));
    }
}
