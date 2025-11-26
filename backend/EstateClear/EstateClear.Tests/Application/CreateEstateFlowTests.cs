using System;
using System.Linq;
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
        Assert.NotEqual(Guid.Empty, result.EstateId.Value);
        Assert.Single(estates.AddedEstates);
        Assert.Equal(executorId, estates.AddedEstates.Single().ExecutorId.Value);
        Assert.Equal(displayName, estates.AddedEstates.Single().DisplayName);
    }
}
