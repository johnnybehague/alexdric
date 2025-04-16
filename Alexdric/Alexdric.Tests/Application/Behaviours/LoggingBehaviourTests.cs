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

    //[TestMethod]
    //public async Task Handle_LogsRequestAndResponse_CallsNextAndReturnsResponse()
    //{
    //    // Arrange
    //    var loggerMock = new Mock<ILogger<LoggingBehaviour<TestRequest, TestResponse>>>();
    //    var loggingBehaviour = new LoggingBehaviour<TestRequest, TestResponse>(loggerMock.Object);

    //    var request = new TestRequest { Message = "Ping" };
    //    var response = new TestResponse { Reply = "Pong" };

    //    var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
    //    nextMock.Setup(x => x(It.IsAny<CancellationToken>())).ReturnsAsync(response);

    //    // Act
    //    var result = await loggingBehaviour.Handle(request, nextMock.Object, CancellationToken.None);

    //    // Assert
    //    Assert.AreEqual(response, result);

    //    loggerMock.Verify(
    //        l => l.Log(
    //            LogLevel.Information,
    //            It.IsAny<EventId>(),
    //            It.Is<It.IsAnyType>((o, _) => o.ToString()!.Contains("Clean Architecture Request Handling")),
    //            null,
    //            It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
    //        Times.Once);

    //    loggerMock.Verify(
    //        l => l.Log(
    //            LogLevel.Information,
    //            It.IsAny<EventId>(),
    //            It.Is<It.IsAnyType>((o, _) => o.ToString()!.Contains("Clean Architecture Response Handling")),
    //            null,
    //            It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
    //        Times.Once);
    //}

    [TestMethod]
    public void Constructor_WithValidLogger_ShouldNotThrow()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<LoggingBehaviour<TestRequest, TestResponse>>>();

        // Act + Assert
        var behaviour = new LoggingBehaviour<TestRequest, TestResponse>(loggerMock.Object);

        Assert.IsNotNull(behaviour);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        // Arrange
        ILogger<LoggingBehaviour<TestRequest, TestResponse>> nullLogger = null;

        // Act
        var behaviour = new LoggingBehaviour<TestRequest, TestResponse>(nullLogger);

        // Assert -> via ExpectedException
    }

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

    //[Fact]
    //public async Task Handle_LogsRequestAndResponse_CallsNextAndReturnsResponse()
    //{
    //    // Arrange
    //    var loggerMock = new Mock<ILogger<LoggingBehaviour<TestRequest, TestResponse>>>();
    //    var loggingBehaviour = new LoggingBehaviour<TestRequest, TestResponse>(loggerMock.Object);

    //    var request = new TestRequest { Message = "Ping" };
    //    var response = new TestResponse { Reply = "Pong" };

    //    var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
    //    nextMock.Setup(n => n()).ReturnsAsync(response);

    //    // Act
    //    var result = await loggingBehaviour.Handle(request, nextMock.Object, CancellationToken.None);

    //    // Assert
    //    Assert.Equal(response, result);

    //    loggerMock.Verify(
    //        l => l.Log(
    //            LogLevel.Information,
    //            It.IsAny<EventId>(),
    //            It.Is<It.IsAnyType>((o, _) => o.ToString()!.Contains("Clean Architecture Request Handling")),
    //            null,
    //            It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
    //        Times.Once);

    //    loggerMock.Verify(
    //        l => l.Log(
    //            LogLevel.Information,
    //            It.IsAny<EventId>(),
    //            It.Is<It.IsAnyType>((o, _) => o.ToString()!.Contains("Clean Architecture Response Handling")),
    //            null,
    //            It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
    //        Times.Once);
    //}
}
