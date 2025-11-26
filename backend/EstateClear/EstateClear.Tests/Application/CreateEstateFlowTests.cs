using System;
using System.Linq;
using System.Threading.Tasks;
using EstateClear.Application;
using Xunit;

namespace EstateClear.Tests.Application;

public class CreateEstateFlowTests
{
    [Fact]
    public async Task AnExecutorCreatingAnEstateProducesANewEstateId()
    {
        var executorId = Guid.NewGuid();
        var displayName = "Estate Alpha";
        var input = new CreateEstate(executorId, displayName);
        var estates = new EstatesFake();
        var flow = new CreateEstateFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.NotNull(result.EstateId);
        Assert.NotEqual(Guid.Empty, result.EstateId.Value());
        Assert.Single(estates.AddedEstates);
        Assert.Equal(executorId, estates.AddedEstates.Single().ExecutorId.Value());
        Assert.Equal("Estate Alpha", estates.AddedEstates.Single().DisplayName);
    }

    [Fact]
    public async Task AnEmptyDisplayNameShouldBeRejected()
    {
        var executorId = Guid.NewGuid();
        var displayName = "   ";
        var input = new CreateEstate(executorId, displayName);
        var estates = new EstatesFake();
        var flow = new CreateEstateFlow(estates);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAnyAsync<Exception>(action);
        Assert.Empty(estates.AddedEstates);
    }

    [Fact]
    public async Task AnEmptyExecutorIdShouldBeRejected()
    {
        var executorId = Guid.Empty;
        var displayName = "Estate Alpha";
        var input = new CreateEstate(executorId, displayName);
        var estates = new EstatesFake();
        var flow = new CreateEstateFlow(estates);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAnyAsync<Exception>(action);
        Assert.Empty(estates.AddedEstates);
    }
}
