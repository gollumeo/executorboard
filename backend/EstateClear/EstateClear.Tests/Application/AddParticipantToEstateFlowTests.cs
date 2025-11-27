using System.Reflection;
using System.Threading.Tasks;
using EstateClear.Application;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;
using Xunit;

namespace EstateClear.Tests.Application;

public class AddParticipantToEstateFlowTests
{
    [Fact]
    public async Task AddParticipantToEstateFlowShouldIncreaseParticipantsCount()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var input = new AddParticipantToEstate(estateId);
        var flow = new AddParticipantToEstateFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.Single(estates.SavedEstates);
        var saved = estates.SavedEstates[0];
        var participantsCount = (int)(saved.GetType()
            .GetField("_participantsCount", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.GetValue(saved) ?? 0);
        Assert.Equal(1, participantsCount);
        Assert.Equal(EstateStatus.Active, saved.Status);
        Assert.Equal("Estate Alpha", saved.DisplayName().Value());
        Assert.Empty(estates.RenamedEstates);
    }
}
