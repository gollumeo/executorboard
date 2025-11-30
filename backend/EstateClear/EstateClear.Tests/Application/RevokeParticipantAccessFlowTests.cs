using System.Collections;
using System.Reflection;
using System.Threading.Tasks;
using EstateClear.Application;
using EstateClear.Domain;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;
using Xunit;

namespace EstateClear.Tests.Application;

public class RevokeParticipantAccessFlowTests
{
    [Fact]
    public async Task RevokeAccessShouldRemoveParticipantAndSaveEstate()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var participant = Participant.From("john.doe@example.com", "John", "Doe");
        var executor = Executor.From(executorId.Value());
        estate.GrantParticipantAccess(participant, executor);
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var input = new RevokeParticipantAccess(estateId, participant, executor);
        var flow = new RevokeParticipantAccessFlow(estates);

        await flow.Execute(input);

        var participantsField = estate
            .GetType()
            .GetField("_participants", BindingFlags.Instance | BindingFlags.NonPublic);
        var participants = participantsField?.GetValue(estate) as IEnumerable;
        var enumerator = participants?.GetEnumerator();

        Assert.NotNull(enumerator);
        Assert.False(enumerator!.MoveNext());
        Assert.Single(estates.SavedEstates);
    }

    [Fact]
    public async Task RevokeAccessShouldFailWhenEstateNotFound()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var participant = Participant.From("jane.doe@example.com", "Jane", "Doe");
        var executor = Executor.From(Guid.NewGuid());
        var estates = new EstatesFake();
        var input = new RevokeParticipantAccess(estateId, participant, executor);
        var flow = new RevokeParticipantAccessFlow(estates);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAnyAsync<Exception>(action);
        Assert.Empty(estates.SavedEstates);
    }

    [Fact]
    public async Task RevokeAccessShouldFailIfParticipantHasContributions()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var participant = Participant.From("johnny.doe@example.com", "Johnny", "Doe");
        var executor = Executor.From(executorId.Value());
        estate.GrantParticipantAccess(participant, executor);

        var contributionsField = estate
            .GetType()
            .GetField("_contributions", BindingFlags.Instance | BindingFlags.NonPublic);
        var contributions = contributionsField?.GetValue(estate) as IList;
        contributions?.Add("dummy");

        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var input = new RevokeParticipantAccess(estateId, participant, executor);
        var flow = new RevokeParticipantAccessFlow(estates);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAsync<DomainException>(action);
        Assert.Empty(estates.SavedEstates);
    }
}
