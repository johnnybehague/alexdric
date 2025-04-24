using Alexdric.Application.Commands;
using Alexdric.Application.DTOs;
using Alexdric.Domain.Entities;
using Alexdric.Domain.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Alexdric.Tests.Application.Commands;

[TestClass]
public class BaseCreateCommandHandlerTests
{
    public class TestEntity : IEntity
    {
        public int Id { get; set; }
    }

    public class TestDto : IDto
    {
        public int Id { get; set; }
    }

    private Mock<ICreateRepository<TestEntity>> _repositoryMock;
    private Mock<IMapper> _mapperMock;
    private BaseCreateCommandHandler<TestEntity, TestDto> _handler;

    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<ICreateRepository<TestEntity>>();
        _mapperMock = new Mock<IMapper>();
        _handler = new BaseCreateCommandHandler<TestEntity, TestDto>(_repositoryMock.Object, _mapperMock.Object);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Success_When_Entity_Created_Successfully()
    {
        // Arrange
        var command = new BaseCreateCommand<TestDto>
        {
            Dto = new TestDto
            {
                Id = 1
            }
        };

        var entity = new TestEntity
        { 
            Id = 1 
        };

        // Mock du mapper pour transformer la commande en entité
        _mapperMock.Setup(m => m.Map<TestEntity>(command.Dto)).Returns(entity);

        // Mock de la méthode CreateAsync du repository
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(EntityState.Added);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Succcess);
        Assert.AreEqual("Create succeed!", result.Message);
        Assert.IsTrue(result.Data);

        _mapperMock.Verify(m => m.Map<TestEntity>(command.Dto), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Failure_When_Entity_Creation_Fails()
    {
        // Arrange
        var command = new BaseCreateCommand<TestDto>
        {
            Dto = new TestDto
            {
                Id = 1
            }
        };

        var entity = new TestEntity
        {
            Id = 1
        };

        // Mock du mapper pour transformer la commande en entité
        _mapperMock.Setup(m => m.Map<TestEntity>(command.Dto)).Returns(entity);

        // Mock de la méthode CreateAsync du repository
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<TestEntity>())).ReturnsAsync(EntityState.Detached); // Échec de la création

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Succcess);
        // Assert.AreEqual("Create failed.", result.Message);
        Assert.IsFalse(result.Data);

        _mapperMock.Verify(m => m.Map<TestEntity>(command.Dto), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
    }

    [TestMethod]
    public async Task Handle_Should_Return_Failure_When_Exception_Is_Thrown()
    {
        // Arrange
        var command = new BaseCreateCommand<TestDto>
        {
            Dto = new TestDto
            {
                Id = 1
            }
        };

        var entity = new TestEntity
        {
            Id = 1
        };

        // Mock du mapper pour transformer la commande en entité
        _mapperMock.Setup(m => m.Map<TestEntity>(command.Dto)).Returns(entity);

        // Mock de la méthode CreateAsync du repository pour lancer une exception
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<TestEntity>())).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Succcess);
        // Assert.AreEqual("Database error", result.Message);
        Assert.IsFalse(result.Data);

        _mapperMock.Verify(m => m.Map<TestEntity>(command.Dto), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<TestEntity>()), Times.Once);
    }

}
