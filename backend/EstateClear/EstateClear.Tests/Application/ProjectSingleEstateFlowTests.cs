using System.Threading.Tasks;
using EstateClear.Application;
using EstateClear.Application.DTOs;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;
using Xunit;

namespace EstateClear.Tests.Application;

public class ProjectSingleEstateFlowTests
{
    [Fact]
    public async Task ProjectingAnExistingEstateShouldReturnProjection()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var input = new ProjectSingleEstate(estateId);
        var flow = new ProjectSingleEstateFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.IsType<SingleEstateProjection>(result);
        Assert.Equal(estateId.Value(), result!.EstateId);
        Assert.Equal(executorId.Value(), result.ExecutorId);
        Assert.Equal("Estate Alpha", result.DisplayName);
        Assert.Equal(estate.Status.ToString(), result.Status);
    }

    [Fact]
    public async Task ProjectingANonExistingEstateShouldReturnNull()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var estates = new EstatesFake
        {
            LoadedEstate = null
        };
        var input = new ProjectSingleEstate(estateId);
        var flow = new ProjectSingleEstateFlow(estates);

        var result = await flow.Execute(input);

        Assert.Null(result);
    }
}
