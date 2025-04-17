using Alexdric.Application.DTOs;
using Alexdric.Application.Queries;
using Alexdric.Domain.Entities;
using AutoMapper;
using Moq;

namespace Alexdric.Tests.Application.Queries;

[TestClass]
public class BaseQueryHandlerTests
{
    #region Test Classes

    public record SampleEntity(string Name) : IEntity;

    public record SampleDto(string Name) : IDto;

    #endregion

    private Mock<IMapper> _mapperMock;
    private BaseQueryHandler<SampleEntity, SampleDto> _handler;

    [TestInitialize]
    public void Setup()
    {
        _mapperMock = new Mock<IMapper>();
        _handler = new BaseQueryHandler<SampleEntity, SampleDto>(_mapperMock.Object);
    }

    #region GetSuccessResponse Tests

    [TestMethod]
    public void GetSuccessResponse_ShouldReturnMappedDto_WhenEntityIsNotNull()
    {
        // Arrange
        var entity = new SampleEntity("Alice");
        var dto = new SampleDto("Alice");

        _mapperMock.Setup(m => m.Map<SampleDto>(entity)).Returns(dto);

        // Act
        var response = _handler.GetSuccessResponse(entity);

        // Assert
        Assert.IsTrue(response.Succcess);
        Assert.AreEqual("Query succeed!", response.Message);
        Assert.AreEqual(dto, response.Data);
    }

    [TestMethod]
    public void GetSuccessResponse_ShouldThrowArgumentNullException_WhenEntityIsNull()
    {
        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => _handler.GetSuccessResponse(null));
    }

    #endregion

    #region GetIEnumerableSuccessResponse Tests

    [TestMethod]
    public void GetIEnumerableSuccessResponse_ShouldReturnMappedDtos_WhenEntitiesAreNotNull()
    {
        // Arrange
        var entities = new List<SampleEntity>
            {
                new SampleEntity("A"),
                new SampleEntity("B")
            };

        var dtos = new List<SampleDto>
            {
                new SampleDto("A"),
                new SampleDto("B")
            };

        _mapperMock.Setup(m => m.Map<IEnumerable<SampleDto>>(entities)).Returns(dtos);

        // Act
        var response = _handler.GetIEnumerableSuccessResponse(entities);

        // Assert
        Assert.IsTrue(response.Succcess);
        Assert.AreEqual("Query succeed!", response.Message);
        CollectionAssert.AreEqual(dtos.ToList(), response.Data.ToList());
    }

    [TestMethod]
    public void GetIEnumerableSuccessResponse_ShouldThrowArgumentNullException_WhenEntitiesAreNull()
    {
        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => _handler.GetIEnumerableSuccessResponse(null));
    }

    #endregion

    #region GetErrorResponse Tests

    [TestMethod]
    public void GetErrorResponse_ShouldReturnErrorResponse()
    {
        // Arrange
        var ex = new Exception("Something went wrong");

        // Act
        var response = _handler.GetErrorResponse(ex);

        // Assert
        Assert.IsFalse(response.Succcess);
        Assert.AreEqual(ex.Message, response.Message);
        Assert.IsNotNull(response.Errors);
        Assert.AreEqual(1, response.Errors.Count());
        Assert.AreEqual(ex.Message, response.Errors.First().ErrorMessage);
    }

    #endregion

    #region GetIEnumerableErrorResponse Tests

    [TestMethod]
    public void GetIEnumerableErrorResponse_ShouldReturnErrorResponse()
    {
        // Arrange
        var ex = new Exception("Error in list");

        // Act
        var response = _handler.GetIEnumerableErrorResponse(ex);

        // Assert
        Assert.IsFalse(response.Succcess);
        Assert.AreEqual(ex.Message, response.Message);
        Assert.IsNotNull(response.Errors);
        Assert.AreEqual(1, response.Errors.Count());
        Assert.AreEqual(ex.Message, response.Errors.First().ErrorMessage);
    }

    #endregion
}
