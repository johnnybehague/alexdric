using Alexdric.Application.Queries;

namespace Alexdric.Tests.Application.Queries;

[TestClass]
public class BaseGetByIdQueryTests
{
    [TestMethod]
    public void BaseGetByIdQuery_CanBeInstantiated_WithId()
    {
        // Arrange
        int expectedId = 42;

        // Act
        var query = new BaseGetByIdQuery { Id = expectedId };

        // Assert
        Assert.AreEqual(expectedId, query.Id);
    }

    [TestMethod]
    public void BaseGetByIdQuery_DefaultConstructor_SetsIdToZero()
    {
        // Act
        var query = new BaseGetByIdQuery();

        // Assert
        Assert.AreEqual(0, query.Id);
    }
}
