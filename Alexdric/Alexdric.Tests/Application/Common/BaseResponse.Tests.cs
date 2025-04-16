using Alexdric.Application.Common;

namespace Alexdric.Tests.Application.Common;

[TestClass]
public class BaseResponseTests
{
    [TestMethod]
    public void Constructor_ShouldReturnsResponseWithDefaultValues_WhenNoParameters()
    {
        // Act
        var response = new BaseResponse<string>();

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
        var data = "Some test data";
        var message = "All good!";
        var errors = new List<BaseError>
            {
                new BaseError { PropertyMessage = "Field1", ErrorMessage = "Required" }
            };

        // Act
        var response = new BaseResponse<string>
        {
            Succcess = true,
            Data = data,
            Message = message,
            Errors = errors
        };

        // Assert
        Assert.IsTrue(response.Succcess);
        Assert.AreEqual(data, response.Data);
        Assert.AreEqual(message, response.Message);
        Assert.AreEqual(1, response.Errors.Count());
        Assert.AreEqual("Field1", response.Errors.First().PropertyMessage);
    }

    [TestMethod]
    public void Constructor_ShouldReturnsResponseWitNullValues_WhenParametersAreNull()
    {
        // Act
        var response = new BaseResponse<object>
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
