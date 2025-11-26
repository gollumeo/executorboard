using EstateClear.Domain;
using EstateClear.Domain.Estates;

namespace EstateClear.Tests.Domain;

public class EstateCreationTests
{
    [Fact]
    public void CreateEstateWithExecutorAndDisplayNameSucceeds()
    {
        // Arrange
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = "Estate Alpha";

        // Act
        var estate = Estate.Create(estateId, executorId, displayName);

        // Assert
        Assert.NotNull(estate);
        Assert.Equal(estateId, estate.Id);
        Assert.Equal(executorId, estate.ExecutorId);
        Assert.Equal(displayName, estate.DisplayName.Value());
        Assert.Equal(EstateStatus.Active, estate.Status);
    }

    [Fact]
    public void CreateEstateWithMissingExecutorThrows()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        ExecutorId? executorId = null;
        const string displayName = "Estate Alpha";

        var exception = Assert.Throws<DomainException>(() => Estate.Create(estateId, executorId!, displayName));

        // Assert
        Assert.Equal("Executor is required", exception.Message);
    }

    [Fact]
    public void CreateEstateWithEmptyDisplayNameThrows()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = "   ";

        var exception = Assert.Throws<DomainException>(() => Estate.Create(estateId, executorId, displayName));

        Assert.Equal("Display name is required", exception.Message);
    }
}
