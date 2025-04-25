using Alexdric.Application.DTOs;
using Alexdric.Application.Queries;
using Alexdric.Domain.Entities;
using Alexdric.Domain.Repositories;
using AutoMapper;
using Moq;

namespace Alexdric.Tests.Application.Queries;

[TestClass]
public class BaseGetAllQueryHandlerTests
{
    // Fake entity & DTO for testing purposes
    public class TestEntity : IEntity { }
    public class TestDto : IDto { }

    private Mock<IQueryRepository<TestEntity>> _repositoryMock;
    private Mock<IMapper> _mapperMock;
    private BaseGetAllQueryHandler<TestEntity, TestDto> _handler;

    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<IQueryRepository<TestEntity>>();
        _mapperMock = new Mock<IMapper>();
        _handler = new BaseGetAllQueryHandler<TestEntity, TestDto>(_repositoryMock.Object, _mapperMock.Object);
    }

    [TestMethod]
    public async Task Handle_ReturnsSuccessResponse_WhenRepositoryReturnsEntities()
    {
        // Arrange
        var fakeEntities = new List<TestEntity> { new TestEntity() };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(fakeEntities);

        // Act
        var result = await _handler.Handle(new BaseGetAllQuery(), CancellationToken.None);

        // Assert
        Assert.IsTrue(result.Succcess);
        Assert.IsNotNull(result.Data);
        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ReturnsErrorResponse_WhenRepositoryThrowsException()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _handler.Handle(new BaseGetAllQuery(), CancellationToken.None);

        // Assert
        Assert.IsFalse(result.Succcess);
        Assert.IsNull(result.Data);
        Assert.IsNotNull(result.Errors);
    }
}