using ExecutorBoard.Application.Estates.Commands;
using ExecutorBoard.Application.Estates.Flows;
using ExecutorBoard.Domain.Estates.Entities;
using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Tests.Application;

public class CloseEstateFlowTests
{
    [Fact]
    public async Task ClosingAnExistingEstateShouldSaveIt()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var aggregate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var estates = new EstatesFake
        {
            LoadedEstate = aggregate
        };
        var input = new CloseEstate(estateId);
        var flow = new CloseEstateFlow(estates);

        await flow.Execute(input);

        Assert.Single(estates.SavedEstates);
        Assert.Equal(estateId, estates.SavedEstates[0].Id);
    }

    [Fact]
    public async Task ClosingANonExistingEstateShouldFail()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var estates = new EstatesFake
        {
            LoadedEstate = null
        };
        var input = new CloseEstate(estateId);
        var flow = new CloseEstateFlow(estates);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAnyAsync<Exception>(action);
        Assert.Empty(estates.SavedEstates);
    }
}
