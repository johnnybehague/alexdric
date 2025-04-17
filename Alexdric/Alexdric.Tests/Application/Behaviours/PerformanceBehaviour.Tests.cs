using Alexdric.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics;

namespace Alexdric.Tests.Application.Behaviours;

[TestClass]
public class PerformanceBehaviourTests
{
    public class TestRequest : IRequest<TestResponse>
    {
        public string Message { get; set; } = "Hello!";
    }

    public class TestResponse
    {
        public string Reply { get; set; } = "Hi!";
    }

    #region Constructor Tests

    [TestMethod]
    public void Constructor_ShouldReturnsNewPerformanceBehaviour_WhenLoggerIsNotNull()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<PerformanceBehaviour<TestRequest, TestResponse>>>();

        // Act + Assert
        var behaviour = new PerformanceBehaviour<TestRequest, TestResponse>(loggerMock.Object);

        Assert.IsNotNull(behaviour);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_ShouldThrowArgumentNullException_WhenLoggerIsNull()
    {
        // Arrange
        ILogger<PerformanceBehaviour<TestRequest, TestResponse>> nullLogger = null;

        // Act
        var behaviour = new PerformanceBehaviour<TestRequest, TestResponse>(nullLogger);

        // Assert handled by ExpectedException
    }

    #endregion

    #region Handle Tests

    [TestMethod]
    public async Task Handle_ShouldReturnsResponse()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<PerformanceBehaviour<TestRequest, TestResponse>>>();
        var behaviour = new PerformanceBehaviour<TestRequest, TestResponse>(loggerMock.Object);

        var request = new TestRequest { Message = "Hello" };
        var response = new TestResponse { Reply = "Hi" };

        var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
        nextMock.Setup(x => x(It.IsAny<CancellationToken>())).ReturnsAsync(response);

        // Act
        var result = await behaviour.Handle(request, nextMock.Object, CancellationToken.None);

        // Assert
        Assert.AreEqual("Hi", result.Reply);
    }

    [TestMethod]
    public async Task Handle_ShouldNotLog()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<PerformanceBehaviour<TestRequest, TestResponse>>>();
        var behaviour = new PerformanceBehaviour<TestRequest, TestResponse>(loggerMock.Object);

        var request = new TestRequest { Message = "Hello" };
        var response = new TestResponse { Reply = "Hi" };

        var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
        nextMock.Setup(x => x(It.IsAny<CancellationToken>())).ReturnsAsync(response);

        // Act
        var result = await behaviour.Handle(request, nextMock.Object, CancellationToken.None);

        // Assert
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Never);
    }

    [TestMethod]
    public async Task Handle_ShouldLogWarning_WhenExecutionIsTooSlow()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<PerformanceBehaviour<TestRequest, TestResponse>>>();
        var behaviour = new PerformanceBehaviour<TestRequest, TestResponse>(loggerMock.Object);

        var request = new TestRequest { Message = "Hello" };
        var response = new TestResponse { Reply = "Hi" };

        var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
        nextMock.Setup(x => x(It.IsAny<CancellationToken>())).Returns(async () =>
        {
            await Task.Delay(100); // Simule un traitement "lent"
            return response;
        });

        // Act
        var result = await behaviour.Handle(request, nextMock.Object, CancellationToken.None);

        // Assert
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Long Running")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    #endregion
}
