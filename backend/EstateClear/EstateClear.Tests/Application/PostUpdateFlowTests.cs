using EstateClear.Application.Commands;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Tests.Application;

public class PostUpdateFlowTests
{
    [Fact]
    public async Task PostUpdateFlowShouldPostAndSaveUpdate()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;

        var update = Update.From("First update");
        var executor = Executor.From(executorId.Value());
        var input = new PostUpdate(estateId, update, executor);
        var flow = new PostUpdateFlow(estates);

        await flow.Execute(input);

        Assert.Single(estates.SavedEstates);
    }
}
