using Alexdric.Application.Common;

namespace Alexdric.Tests.Application.Common;

[TestClass]
public class BaseErrorTests
{
    #region Constructor Tests

    [TestMethod]
    public void Constructor_ShouldReturnsNewBaseError_WhenNoParameters()
    {
        // Act
        var error = new BaseError();

        // Assert
        Assert.IsNotNull(error);
        Assert.IsNull(error.PropertyMessage);
        Assert.IsNull(error.ErrorMessage);
    }

    [TestMethod]
    public void Constructor_ShouldSetParameters_WhenParametersAreGiven()
    {
        // Arrange
        var expectedProperty = "FieldName";
        var expectedError = "Field is required";

        // Act
        var error = new BaseError
        {
            PropertyMessage = expectedProperty,
            ErrorMessage = expectedError
        };

        // Assert
        Assert.AreEqual(expectedProperty, error.PropertyMessage);
        Assert.AreEqual(expectedError, error.ErrorMessage);
    }

    #endregion
}