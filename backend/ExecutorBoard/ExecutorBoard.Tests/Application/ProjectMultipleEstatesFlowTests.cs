using ExecutorBoard.Application.Estates.DTOs;
using ExecutorBoard.Application.Estates.Queries;
using ExecutorBoard.Domain.Estates.Entities;
using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Tests.Application;

public class ProjectMultipleEstatesFlowTests
{
    [Fact]
    public async Task ProjectingMultipleEstatesForExecutorShouldReturnSummaries()
    {
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estateAId = EstateId.From(Guid.NewGuid());
        var estateBId = EstateId.From(Guid.NewGuid());
        var estateA = Estate.Create(estateAId, executorId, EstateName.From("Estate Alpha"));
        var estateB = Estate.Create(estateBId, executorId, EstateName.From("Estate Beta"));
        var estates = new EstatesFake();
        estates.EstatesById[estateAId] = estateA;
        estates.EstatesById[estateBId] = estateB;
        var input = new ProjectMultipleEstates(executorId);
        var flow = new ProjectMultipleEstatesFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.All(result, item => Assert.IsType<EstateSummaryProjection>(item));
        Assert.Contains(result, r => r.EstateId == estateAId.Value() && r.DisplayName == "Estate Alpha" && r.Status == estateA.Status.ToString());
        Assert.Contains(result, r => r.EstateId == estateBId.Value() && r.DisplayName == "Estate Beta" && r.Status == estateB.Status.ToString());
    }

    [Fact]
    public async Task ProjectingWhenNoEstatesForExecutorShouldReturnEmptyList()
    {
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estates = new EstatesFake();
        var input = new ProjectMultipleEstates(executorId);
        var flow = new ProjectMultipleEstatesFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
