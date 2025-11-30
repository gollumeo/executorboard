using EstateClear.Application.Commands;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Tests.Application;

public class RenameEstateFlowTests
{
    [Fact]
    public async Task RenameEstateFlowShouldUseDomainAggregateForRenaming()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var aggregate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var input = new RenameEstate(estateId, "Estate Beta");
        var estates = new EstatesFake
        {
            LoadedEstate = aggregate
        };
        var flow = new RenameEstateFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.Single(estates.SavedEstates);
        Assert.Equal("Estate Beta", estates.SavedEstates[0].DisplayName().Value());
        Assert.Empty(estates.RenamedEstates);
    }

    [Fact]
    public async Task RenameEstateFlowShouldRejectRenamingAClosedEstate()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var aggregate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        aggregate
            .GetType()
            .GetField("_status", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            ?.SetValue(aggregate, EstateStatus.Closed);
        var input = new RenameEstate(estateId, "Estate Beta");
        var estates = new EstatesFake
        {
            LoadedEstate = aggregate
        };
        var flow = new RenameEstateFlow(estates);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAnyAsync<Exception>(action);
        Assert.Empty(estates.SavedEstates);
        Assert.Empty(estates.RenamedEstates);
    }

    [Fact]
    public async Task RenameEstateFlowShouldDoNothingWhenRenamingToSameName()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var aggregate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var input = new RenameEstate(estateId, "Estate Alpha");
        var estates = new EstatesFake
        {
            LoadedEstate = aggregate
        };
        var flow = new RenameEstateFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.Empty(estates.SavedEstates);
        Assert.Empty(estates.RenamedEstates);
    }

    [Fact]
    public async Task RenamingOneEstateShouldNotAffectAnotherEstateOfSameExecutor()
    {
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estateAId = EstateId.From(Guid.NewGuid());
        var estateBId = EstateId.From(Guid.NewGuid());
        var estateA = Estate.Create(estateAId, executorId, EstateName.From("Estate Alpha"));
        var estateB = Estate.Create(estateBId, executorId, EstateName.From("Estate Beta"));
        var input = new RenameEstate(estateAId, "Estate Alpha Prime");
        var estates = new EstatesFake();
        estates.EstatesById[estateAId] = estateA;
        estates.EstatesById[estateBId] = estateB;
        var flow = new RenameEstateFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.Single(estates.SavedEstates);
        Assert.Equal("Estate Alpha Prime", estates.SavedEstates[0].DisplayName().Value());
        Assert.Equal("Estate Beta", estates.EstatesById[estateBId].DisplayName().Value());
        Assert.Empty(estates.RenamedEstates);
    }

    [Fact]
    public async Task RenamingEstateShouldRejectDuplicateNameForSameExecutor()
    {
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estateAId = EstateId.From(Guid.NewGuid());
        var estateBId = EstateId.From(Guid.NewGuid());
        var estateA = Estate.Create(estateAId, executorId, EstateName.From("Estate Alpha"));
        var estateB = Estate.Create(estateBId, executorId, EstateName.From("Estate Beta"));
        var input = new RenameEstate(estateAId, "Estate Beta");
        var estates = new EstatesFake();
        estates.EstatesById[estateAId] = estateA;
        estates.EstatesById[estateBId] = estateB;
        var flow = new RenameEstateFlow(estates);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAnyAsync<Exception>(action);
        Assert.Empty(estates.SavedEstates);
        Assert.Empty(estates.RenamedEstates);
        Assert.Equal("Estate Beta", estates.EstatesById[estateBId].DisplayName().Value());
        Assert.Equal("Estate Alpha", estates.EstatesById[estateAId].DisplayName().Value());
    }
}
