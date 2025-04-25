using Alexdric.Application.DTOs;
using Alexdric.Application.Queries;
using Alexdric.Domain.Entities;
using Alexdric.Domain.Repositories;
using AutoMapper;
using Moq;

namespace Alexdric.Tests.Application.Queries;

[TestClass]
public class BaseGetByIdQueryHandlerTests
{
    // Fake entity & DTO for testing purposes
    public class TestEntity : IEntity { }
    public class TestDto : IDto { }

    private Mock<IQueryRepository<TestEntity>> _repositoryMock;
    private Mock<IMapper> _mapperMock;
    private BaseGetByIdQueryHandler<TestEntity, TestDto> _handler;

    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<IQueryRepository<TestEntity>>();
        _mapperMock = new Mock<IMapper>();
        _handler = new BaseGetByIdQueryHandler<TestEntity, TestDto>(_repositoryMock.Object, _mapperMock.Object);
    }

    [TestMethod]
    public async Task Handle_ReturnsSuccessResponse_WhenEntityIsFound()
    {
        // Arrange
        var fakeEntity = new TestEntity();
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(fakeEntity);
        _mapperMock.Setup(m => m.Map<TestDto>(It.IsAny<TestEntity>())).Returns(new TestDto());

        var request = new BaseGetByIdQuery { Id = 1 };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.Succcess);
        Assert.IsNotNull(result.Data);
        _repositoryMock.Verify(r => r.GetByIdAsync(1), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ReturnsErrorResponse_WhenExceptionIsThrown()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));

        var request = new BaseGetByIdQuery { Id = 999 };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.Succcess);
        Assert.IsNull(result.Data);
        Assert.IsNotNull(result.Errors);
    }
}
