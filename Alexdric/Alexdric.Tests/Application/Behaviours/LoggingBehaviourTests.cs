using Alexdric.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace Alexdric.Tests.Application.Behaviours;

[TestClass]
public class LoggingBehaviourTests
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
    public void Constructor_ShouldReturnsNewLoggingBehaviour_WhenLoggerIsNotNull()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<LoggingBehaviour<TestRequest, TestResponse>>>();

        // Act + Assert
        var behaviour = new LoggingBehaviour<TestRequest, TestResponse>(loggerMock.Object);

        Assert.IsNotNull(behaviour);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_ShouldThrowArgumentNullException_WhenLoggerIsNull()
    {
        // Arrange
        ILogger<LoggingBehaviour<TestRequest, TestResponse>> nullLogger = null;

        // Act
        var behaviour = new LoggingBehaviour<TestRequest, TestResponse>(nullLogger);

        // Assert -> via ExpectedException
    }

    #endregion

    #region Handle Tests

    [TestMethod]
    public async Task Handle_ShouldReturnsResponse()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<LoggingBehaviour<TestRequest, TestResponse>>>();
        var loggingBehaviour = new LoggingBehaviour<TestRequest, TestResponse>(loggerMock.Object);

        var request = new TestRequest { Message = "Ping" };
        var response = new TestResponse { Reply = "Pong" };

        var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
        nextMock.Setup(x => x(It.IsAny<CancellationToken>())).ReturnsAsync(response);

        // Act
        var result = await loggingBehaviour.Handle(request, nextMock.Object, CancellationToken.None);

        // Assert
        Assert.AreEqual(response, result);
    }

    [TestMethod]
    public async Task Handle_ShouldLogInformations()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<LoggingBehaviour<TestRequest, TestResponse>>>();
        var loggingBehaviour = new LoggingBehaviour<TestRequest, TestResponse>(loggerMock.Object);

        var request = new TestRequest { Message = "Ping" };
        var response = new TestResponse { Reply = "Pong" };

        var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
        nextMock.Setup(x => x(It.IsAny<CancellationToken>())).ReturnsAsync(response);

        // Act
        var result = await loggingBehaviour.Handle(request, nextMock.Object, CancellationToken.None);

        // Assert
        loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, _) => o.ToString()!.Contains("Clean Architecture Request Handling")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);

        loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, _) => o.ToString()!.Contains("Clean Architecture Response Handling")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    #endregion
}
