using Alexdric.Domain.Entities;
using Moq;

namespace Alexdric.Tests.Domain.Entities;

[TestClass]
public class IEntityTests
{
    public class User : IEntity
    {
        public int Id { get; set; }
    }

    [TestMethod]
    public void Mocking_IEntity_ShouldSucceed()
    {
        // Arrange
        var mock = new Mock<IEntity>();

        // Act
        var entity = mock.Object;

        // Assert
        Assert.IsInstanceOfType(entity, typeof(IEntity));
    }

    [TestMethod]
    public void User_ShouldImplementIEntity()
    {
        // Arrange
        var user = new User();

        // Act & Assert
        Assert.IsInstanceOfType(user, typeof(IEntity));
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
