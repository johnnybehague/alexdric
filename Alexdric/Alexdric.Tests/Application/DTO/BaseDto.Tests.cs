using Alexdric.Application.DTOs;

namespace Alexdric.Tests.Application.DTO;

[TestClass]
public class BaseDtoTests
{
    [TestMethod]
    public void Constructor_ShouldReturnsNewBaseDto()
    {
        // Act
        var dto = new BaseDto();

        // Assert
        Assert.IsNotNull(dto);
    }
}
