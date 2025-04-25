using Alexdric.Application.Queries;

namespace Alexdric.Tests.Application.Queries;

[TestClass]
public class BaseGetAllQueryTests
{
    [TestMethod]
    public void BaseGetAllQuery_CanBeInstantiated()
    {
        // Act
        var query = new BaseGetAllQuery();

        // Assert
        Assert.IsNotNull(query);
    }
}
