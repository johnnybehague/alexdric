using Alexdric.Application.Commands;
using Alexdric.Application.Common;
using Alexdric.Application.DTOs;
using Alexdric.Domain.Entities;
using MediatR;

namespace Alexdric.Tests.Application.Commands;

public class BaseCreateCommandTests
{
    public class TestDto : IDto
    {
        public int Id { get; set; }
    }

    [TestMethod]
    public void Command_ShouldStoreAndReturnIdCorrectly()
    {
        
        // Arrange
        var command = new BaseCreateCommand<TestDto>
        {
            Dto = new TestDto { Id = 1 }
        };

        // Act & Assert
        Assert.AreEqual(1, command.Dto.Id);
    }

    [TestMethod]
    public void Command_ShouldImplementIRequest()
    {
        // Arrange
        var command = new BaseCreateCommand<TestDto>();

        // Act & Assert
        Assert.IsInstanceOfType(command, typeof(IRequest<BaseResponse<bool>>));
    }
}
