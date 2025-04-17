using Alexdric.Application.DTOs;
using Moq;

namespace Alexdric.Tests.Application.DTO;

[TestClass]
public class IDtoTests
{
    public class User : IDto
    {
        public int Id { get; set; }
    }

    [TestMethod]
    public void Mocking_IDto_ShouldSucceed()
    {
        // Arrange
        var mock = new Mock<IDto>();

        // Act
        var entity = mock.Object;

        // Assert
        Assert.IsInstanceOfType(entity, typeof(IDto));
    }

    [TestMethod]
    public void User_ShouldImplementIDto()
    {
        // Arrange
        var user = new User();

        // Act & Assert
        Assert.IsInstanceOfType(user, typeof(IDto));
    }

    [TestMethod]
    public void User_CanSetAndGetProperties()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
        };

        // Assert
        Assert.AreEqual(1, user.Id);
    }
}
