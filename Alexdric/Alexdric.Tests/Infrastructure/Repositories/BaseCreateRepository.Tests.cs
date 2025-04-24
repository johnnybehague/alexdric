using Alexdric.Domain.Entities;
using Alexdric.Infrastructure.Repositories;

namespace Alexdric.Tests.Infrastructure.Repositories;

[TestClass]
public class BaseCreateRepositoryTests
{
    public class SampleEntity : IEntity { }

    private BaseCreateRepository<SampleEntity> _repository;

    [TestInitialize]
    public void Setup()
    {
        _repository = new BaseCreateRepository<SampleEntity>();
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public async Task CreateAsync_ShouldThrowNotImplementedException()
    {
        // Arrange
        var entity = new SampleEntity();

        // Act
        await _repository.CreateAsync(entity);

        // Assert is handled by ExpectedException
    }
}
