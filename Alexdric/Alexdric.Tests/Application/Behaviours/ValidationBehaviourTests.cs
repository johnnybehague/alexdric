using Alexdric.Application.Behaviours;
using Alexdric.Application.Exceptions;
using FluentValidation.Results;
using FluentValidation;
using MediatR;
using Moq;

namespace Alexdric.Tests.Application.Behaviours;

[TestClass]
public class ValidationBehaviourTests
{
    // Dummy types
    public class TestRequest : IRequest<TestResponse>
    {
        public string Name { get; set; }
    }

    public class TestResponse { }

    #region Constructor Tests

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_ShouldThrowNewArgumentNullException_WhenValidatorsAreNull()
    {
        // Act
        var behaviour = new ValidationBehaviour<TestRequest, TestResponse>(null);
    }

    [TestMethod]
    public void Constructor_ShouldReturnsNewValidationBehaviour_WhenValidatorsAreNotNull()
    {
        // Arrange
        var validatorMock = new Mock<IEnumerable<IValidator<TestRequest>>>();

        // Act + Assert
        var behaviour = new ValidationBehaviour<TestRequest, TestResponse>(validatorMock.Object);

        Assert.IsNotNull(behaviour);
    }

    #endregion

    #region Handle Tests

    [TestMethod]
    public async Task Handle_ShouldCallNext_WhenValidatorsAreEmpty()
    {
        // Arrange
        var validators = new List<IValidator<TestRequest>>();
        var behaviour = new ValidationBehaviour<TestRequest, TestResponse>(validators);

        var request = new TestRequest { Name = "Test" };
        var response = new TestResponse();

        var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
        nextMock.Setup(x => x(It.IsAny<CancellationToken>())).ReturnsAsync(response);

        // Act
        var result = await behaviour.Handle(request, nextMock.Object, CancellationToken.None);

        // Assert
        Assert.AreEqual(response, result);
        nextMock.Verify(x => x(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ShouldCallNext_WhenValidatorsAreValid()
    {
        // Arrange
        var validatorMock = new Mock<IValidator<TestRequest>>();
        validatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var validators = new List<IValidator<TestRequest>> { validatorMock.Object };
        var behaviour = new ValidationBehaviour<TestRequest, TestResponse>(validators);

        var request = new TestRequest { Name = "Valid" };
        var response = new TestResponse();

        var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
        nextMock.Setup(x => x(It.IsAny<CancellationToken>())).ReturnsAsync(response);

        // Act
        var result = await behaviour.Handle(request, nextMock.Object, CancellationToken.None);

        // Assert
        Assert.AreEqual(response, result);
        nextMock.Verify(x => x(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ShouldThrowCustomValidationException_WhenValidatorsAreNotValid()
    {
        // Arrange
        var validationFailure = new ValidationFailure("Name", "Name is required");
        var validationResult = new ValidationResult(new List<ValidationFailure> { validationFailure });

        var validatorMock = new Mock<IValidator<TestRequest>>();
        validatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        var validators = new List<IValidator<TestRequest>> { validatorMock.Object };
        var behaviour = new ValidationBehaviour<TestRequest, TestResponse>(validators);

        var request = new TestRequest { Name = "" };
        var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();

        // Act & Assert
        try
        {
            var result = await behaviour.Handle(request, nextMock.Object, CancellationToken.None);
            Assert.Fail("Expected CustomValidationException to be thrown.");
        }
        catch (CustomValidationException ex)
        {
            Assert.AreEqual(1, ex.Errors.Count());
            var error = ex.Errors.First();
            Assert.AreEqual("Name", error.PropertyMessage);
            Assert.AreEqual("Name is required", error.ErrorMessage);
            nextMock.Verify(x => x(It.IsAny<CancellationToken>()), Times.Never);
        }
    }

    #endregion
}
