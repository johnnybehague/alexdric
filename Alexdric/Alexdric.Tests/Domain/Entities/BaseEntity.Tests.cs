using Alexdric.Domain.Entities;

namespace Alexdric.Tests.Domain.Entities;

[TestClass]
public class BaseEntityTests
{
    [TestMethod]
    public void Constructor_ShouldReturnsNewBaseEntity()
    {
        // Act
        var entity = new BaseEntity();

        // Assert
        Assert.IsNotNull(entity);
    }
}
