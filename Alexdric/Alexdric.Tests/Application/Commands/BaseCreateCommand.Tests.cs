using Alexdric.Application.Commands;
using Alexdric.Application.Common;
using Alexdric.Domain.Entities;
using MediatR;

namespace Alexdric.Tests.Application.Commands;

public class BaseCreateCommandTests
{
    public class EntityTest : IEntity
    {
        public int Id { get; set; }
    }

    [TestMethod]
    public void Command_ShouldStoreAndReturnIdCorrectly()
    {
        
        // Arrange
        var command = new BaseCreateCommand<EntityTest>
        {
            Entity = new EntityTest { Id = 1 }
        };

        // Act & Assert
        Assert.AreEqual(1, command.Entity.Id);
    }

    [TestMethod]
    public void Command_ShouldImplementIRequest()
    {
        // Arrange
        var command = new BaseCreateCommand<EntityTest>();

        // Act & Assert
        Assert.IsInstanceOfType(command, typeof(IRequest<BaseResponse<bool>>));
    }
}
