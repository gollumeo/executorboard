using EstateClear.Domain;
using EstateClear.Domain.Estates;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Tests.Domain;

public class EstateCreationTests
{
    [Fact]
    public void CreateEstateWithExecutorAndDisplayNameSucceeds()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = EstateName.From("Estate Alpha");

        var estate = Estate.Create(estateId, executorId, displayName);

        Assert.NotNull(estate);
        Assert.Equal(estateId, estate.Id);
        Assert.Equal(executorId, estate.ExecutorId);
        Assert.Equal(displayName.Value(), estate.DisplayName().Value());
        Assert.Equal(EstateStatus.Active, estate.Status);
    }

    [Fact]
    public void CreateEstateWithMissingExecutorThrows()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        ExecutorId? executorId = null;
        var displayName = EstateName.From("Estate Alpha");
        var action = () => Estate.Create(estateId, executorId!, displayName);
        var exception = Assert.Throws<DomainException>(() => Estate.Create(estateId, executorId!, displayName));

        Assert.Equal("Executor is required", exception.Message);
    }

    [Fact]
    public void CreateEstateWithEmptyDisplayNameThrows()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());

        var exception = Assert.Throws<DomainException>(() => Estate.Create(estateId, executorId, EstateName.From("   ")));

        Assert.Equal("Display name is required", exception.Message);
    }
}
