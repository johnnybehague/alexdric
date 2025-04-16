using Alexdric.Application.Common;
using Alexdric.Application.Exceptions;

namespace Alexdric.Tests.Application.Exceptions;

[TestClass]
public class CustomValidationExceptionTests
{
    [TestMethod]
    public void Constructor_ShouldSetDefaultMessageAndEmptyErrors_WhenNoParameters()
    {
        // Act
        var exception = new CustomValidationException();

        // Assert
        Assert.AreEqual("One or more validation failures have occured.", exception.Message);
        Assert.IsNotNull(exception.Errors);
        Assert.AreEqual(0, exception.Errors.Count());
    }

    [TestMethod]
    public void Constructor_ShouldSetErrorsCorrectly_WhenErrorsAreGiven()
    {
        // Arrange
        var errors = new List<BaseError>
            {
                new BaseError { PropertyMessage = "Email", ErrorMessage = "Email is required" },
                new BaseError { PropertyMessage = "Password", ErrorMessage = "Password must be at least 6 characters" }
            };

        // Act
        var exception = new CustomValidationException(errors);

        // Assert
        Assert.AreEqual("One or more validation failures have occured.", exception.Message);
        Assert.IsNotNull(exception.Errors);
        Assert.AreEqual(2, exception.Errors.Count());

        var first = exception.Errors.First();
        Assert.AreEqual("Email", first.PropertyMessage);
        Assert.AreEqual("Email is required", first.ErrorMessage);
    }

    [TestMethod]
    public void CustomValidationException_ShouldBeExceptionType()
    {
        // Act
        var exception = new CustomValidationException();

        // Assert
        Assert.IsInstanceOfType(exception, typeof(Exception));
    }
}
