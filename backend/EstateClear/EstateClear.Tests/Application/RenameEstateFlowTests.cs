using EstateClear.Application;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Tests.Application;

public class RenameEstateFlowTests
{
    [Fact]
    public async Task RenamingAnEstateShouldUpdateItsName()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var currentName = EstateName.From("Estate Alpha");
        var newName = "Estate Beta";
        var input = new RenameEstate(estateId, newName);
        var estates = new EstatesFake();
        estates.AddedEstates.Add((estateId, executorId, currentName.Value()));
        var flow = new RenameEstateFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.Single(estates.RenamedEstates);
        Assert.Equal(estateId, estates.RenamedEstates[0].EstateId);
        Assert.Equal(EstateName.From(newName).Value(), estates.RenamedEstates[0].NewName.Value());
    }

    [Fact]
    public async Task RenamingAnEstateToAnExistingNameForSameExecutorShouldBeRejected()
    {
        var executorId = ExecutorId.From(Guid.NewGuid());
        var targetEstateId = EstateId.From(Guid.NewGuid());
        var otherEstateId = EstateId.From(Guid.NewGuid());
        var currentName = EstateName.From("Estate Alpha");
        var existingName = EstateName.From("Estate Beta");
        var input = new RenameEstate(targetEstateId, "estate beta");
        var estates = new EstatesFake();
        estates.AddedEstates.Add((targetEstateId, executorId, currentName.Value()));
        estates.AddedEstates.Add((otherEstateId, executorId, existingName.Value()));
        var flow = new RenameEstateFlow(estates);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAnyAsync<Exception>(action);
        Assert.Empty(estates.RenamedEstates);
    }

    [Fact]
    public async Task RenamingAnEstateToItsCurrentNameShouldDoNothing()
    {
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estateId = EstateId.From(Guid.NewGuid());
        var currentName = EstateName.From("Estate Alpha");
        var input = new RenameEstate(estateId, "  eStATe   ALpha  ");
        var estates = new EstatesFake();
        estates.AddedEstates.Add((estateId, executorId, currentName.Value()));
        var flow = new RenameEstateFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.Empty(estates.RenamedEstates);
    }
}
