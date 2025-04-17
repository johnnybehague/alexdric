using Alexdric.Application.Common;

namespace Alexdric.Tests.Application.Common;

[TestClass]
public class BaseResponseGenericTests
{
    [TestMethod]
    public void Constructor_ShouldReturnsResponseWithDefaultValues_WhenNoParameters()
    {
        // Act
        var response = new BaseReponseGeneric<string>();

        // Assert
        Assert.IsNotNull(response);
        Assert.IsFalse(response.Succcess);
        Assert.IsNull(response.Data);
        Assert.IsNull(response.Message);
        Assert.IsNull(response.Errors);
    }

    [TestMethod]
    public void Constructor_ShouldReturnsResponseWithSetValues_WhenParametersAreNotEmpty()
    {
        // Arrange
        var expectedData = "Test data";
        var expectedMessage = "OK";
        var expectedErrors = new List<BaseError>
            {
                new BaseError { PropertyMessage = "Field", ErrorMessage = "Required" }
            };

        // Act
        var response = new BaseReponseGeneric<string>
        {
            Succcess = true,
            Data = expectedData,
            Message = expectedMessage,
            Errors = expectedErrors
        };

        // Assert
        Assert.IsTrue(response.Succcess);
        Assert.AreEqual(expectedData, response.Data);
        Assert.AreEqual(expectedMessage, response.Message);

        Assert.IsNotNull(response.Errors);
        Assert.AreEqual(1, response.Errors.Count());
        Assert.AreEqual("Field", response.Errors.First().PropertyMessage);
    }

    [TestMethod]
    public void Constructor_ShouldReturnsResponseWitNullValues_WhenParametersAreNull()
    {
        // Act
        var response = new BaseReponseGeneric<object>
        {
            Succcess = false,
            Data = null,
            Message = null,
            Errors = null
        };

        // Assert
        Assert.IsFalse(response.Succcess);
        Assert.IsNull(response.Data);
        Assert.IsNull(response.Message);
        Assert.IsNull(response.Errors);
    }
}
