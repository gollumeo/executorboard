using System.Collections;
using System.Reflection;
using EstateClear.Domain;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Tests.Domain;

public class EstateParticipantTests
{
    [Fact]
    public void GrantParticipantAccessShouldEnforceExecutorAuthority()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        var participant = Participant.From("jane.doe@example.com", "Jane", "Doe");
        var otherExecutor = Executor.From(Guid.NewGuid());

        Assert.Throws<DomainException>(() => estate.GrantParticipantAccess(participant, otherExecutor));

        var authorizedExecutor = Executor.From(executorId.Value());
        estate.GrantParticipantAccess(participant, authorizedExecutor);

        var participants = estate.Participants();
        Assert.Single(participants);
        Assert.Equal(participant, participants[0]);
    }

    [Fact]
    public void GrantParticipantAccessShouldEnforceUniqueness()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var executor = Executor.From(executorId.Value());

        var participant = Participant.From("john.doe@example.com", "John", "Doe");
        estate.GrantParticipantAccess(participant, executor);

        var duplicate = Participant.From("john.doe@example.com", "Johnny", "Doe");

        Assert.Throws<DomainException>(() => estate.GrantParticipantAccess(duplicate, executor));
        Assert.Single(estate.Participants());
    }

    [Fact]
    public void RevokeParticipantAccessShouldEnforceExecutorAuthority()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        var participant = Participant.From("jane.doe@example.com", "Jane", "Doe");
        var executor = Executor.From(executorId.Value());
        estate.GrantParticipantAccess(participant, executor);

        var otherExecutor = Executor.From(Guid.NewGuid());

        Assert.Throws<DomainException>(() => estate.RevokeParticipantAccess(participant, otherExecutor));
        Assert.Single(estate.Participants());

        estate.RevokeParticipantAccess(participant, executor);
        Assert.Empty(estate.Participants());
    }

    [Fact]
    public void ParticipantWithContributionsCannotBeRevoked()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        var participant = Participant.From("jane.doe@example.com", "Jane", "Doe");
        var executor = Executor.From(executorId.Value());
        estate.GrantParticipantAccess(participant, executor);

        var contributionsField = typeof(Estate)
            .GetField("_contributions", BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.NotNull(contributionsField);

        var contributions = contributionsField!.GetValue(estate) as IList;
        Assert.NotNull(contributions);

        contributions!.Add("dummy");

        Assert.Throws<DomainException>(() => estate.RevokeParticipantAccess(participant, executor));

        Assert.Single(estate.Participants());
    }

    [Fact]
    public void ParticipantWithUpdateMustRespectPostingRules()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        var update = Update.From("First update");
        var executor = Executor.From(executorId.Value());

        estate.PostUpdate(update, executor);

        var updatesField = typeof(Estate)
            .GetField("_updates", BindingFlags.Instance | BindingFlags.NonPublic);

        Assert.NotNull(updatesField);

        var updates = updatesField!.GetValue(estate) as IEnumerable;
        Assert.NotNull(updates);

        var enumerator = updates!.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(update, enumerator.Current);

        typeof(Estate)
            .GetField("_status", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(estate, EstateStatus.Closed);

        Assert.Throws<DomainException>(() => estate.PostUpdate(update, executor));

        var unauthorizedExecutor = Executor.From(Guid.NewGuid());

        Assert.Throws<DomainException>(() => estate.PostUpdate(update, unauthorizedExecutor));
    }

    [Fact]
    public void CloseShouldRemoveParticipants()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var executor = Executor.From(executorId.Value());

        estate.GrantParticipantAccess(Participant.From("john.doe@example.com", "John", "Doe"), executor);
        estate.GrantParticipantAccess(Participant.From("jane.doe@example.com", "Jane", "Doe"), executor);

        estate.Close();

        Assert.Empty(estate.Participants());
        Assert.Equal(EstateStatus.Closed, estate.Status);
    }
}
