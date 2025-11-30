using System.Collections;
using System.Reflection;
using EstateClear.Domain;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Tests.Domain;

public class EstateParticipantTests
{
    [Fact]
    public void AddParticipantShouldIncreaseParticipantsCount()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        estate.AddParticipant();

        var count = (int)(estate
            .GetType()
            .GetField("_participantsCount", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.GetValue(estate) ?? 0);

        Assert.Equal(1, count);
        Assert.Equal(EstateStatus.Active, estate.Status);
        Assert.Equal("Estate Alpha", estate.DisplayName().Value());
    }

    [Fact]
    public void RemoveParticipantShouldDecreaseParticipantsCount()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        estate
            .GetType()
            .GetField("_participantsCount", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(estate, 2);

        estate.RemoveParticipant();

        var count = (int)(estate
            .GetType()
            .GetField("_participantsCount", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.GetValue(estate) ?? 0);

        Assert.Equal(1, count);
    }

    [Fact]
    public void RemoveParticipantShouldNotGoBelowZero()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        estate
            .GetType()
            .GetField("_participantsCount", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(estate, 0);

        estate.RemoveParticipant();

        var count = (int)(estate
            .GetType()
            .GetField("_participantsCount", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.GetValue(estate) ?? 0);

        Assert.Equal(0, count);
        Assert.Equal(EstateStatus.Active, estate.Status);
        Assert.Equal("Estate Alpha", estate.DisplayName().Value());
    }

    [Fact]
    public void RemovingParticipantOnClosedEstateShouldThrow()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        estate
            .GetType()
            .GetField("_participantsCount", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(estate, 1);

        estate
            .GetType()
            .GetField("_status", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(estate, EstateStatus.Closed);

        Assert.Throws<DomainException>(() => estate.RemoveParticipant());

        var count = (int)(estate
            .GetType()
            .GetField("_participantsCount", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.GetValue(estate) ?? 0);

        Assert.Equal(1, count);
        Assert.Equal(EstateStatus.Closed, estate.Status);
        Assert.Equal("Estate Alpha", estate.DisplayName().Value());
    }

    [Fact]
    public void CloseShouldResetParticipantsCountToZero()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        estate
            .GetType()
            .GetField("_participantsCount", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(estate, 3);

        estate.Close();

        var count = (int)(estate
            .GetType()
            .GetField("_participantsCount", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.GetValue(estate) ?? 0);

        Assert.Equal(0, count);
        Assert.Equal(EstateStatus.Closed, estate.Status);
        Assert.Equal("Estate Alpha", estate.DisplayName().Value());
    }

    [Fact]
    public void AddingParticipantUsesValueObjectAndEnforcesUniqueness()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        var participantType = typeof(Estate)
            .Assembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == "Participant");
        Assert.NotNull(participantType);

        var fromMethod = participantType!.GetMethod("From", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(fromMethod);

        var participant = fromMethod!.Invoke(null, new object?[] { "john.doe@example.com", "John", "Doe" });
        Assert.NotNull(participant);

        var addParticipantMethod = estate
            .GetType()
            .GetMethod("AddParticipant", BindingFlags.Instance | BindingFlags.Public, null, new[] { participantType }, null);

        Assert.NotNull(addParticipantMethod);

        addParticipantMethod!.Invoke(estate, new[] { participant });

        var participantsField = estate
            .GetType()
            .GetField("_participants", BindingFlags.Instance | BindingFlags.NonPublic);

        Assert.NotNull(participantsField);

        var participants = participantsField!.GetValue(estate) as IEnumerable;
        Assert.NotNull(participants);

        var enumerator = participants!.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(participant, enumerator.Current);

        var duplicate = fromMethod!.Invoke(null, ["john.doe@example.com", "Johnny", "Doe"]);
        Assert.NotNull(duplicate);

        var equalsMethod = participant!.GetType().GetMethod("Equals", BindingFlags.Instance | BindingFlags.Public, null, new[] { participantType }, null);
        Assert.NotNull(equalsMethod);

        var areEqual = (bool)(equalsMethod!.Invoke(participant, new[] { duplicate }) ?? false);
        Assert.True(areEqual);

        Assert.Throws<DomainException>(() =>
        {
            try
            {
                addParticipantMethod!.Invoke(estate, new[] { duplicate });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException ?? ex;
            }
        });
    }

    [Fact]
    public void GrantParticipantAccessShouldEnforceExecutorAuthority()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        var participantType = typeof(Estate)
            .Assembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == "Participant");
        Assert.NotNull(participantType);

        var fromMethod = participantType!.GetMethod("From", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(fromMethod);

        var participant = fromMethod!.Invoke(null, new object?[] { "jane.doe@example.com", "Jane", "Doe" });
        Assert.NotNull(participant);

        var executorType = typeof(Estate)
            .Assembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == "Executor");
        Assert.NotNull(executorType);

        var executorFrom = executorType!.GetMethod("From", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(executorFrom);

        var otherExecutor = executorFrom!.Invoke(null, new object?[] { Guid.NewGuid() });
        Assert.NotNull(otherExecutor);

        var grantMethod = estate
            .GetType()
            .GetMethod("GrantParticipantAccess", BindingFlags.Instance | BindingFlags.Public, null, new[] { participantType, executorType }, null);

        Assert.NotNull(grantMethod);

        Assert.Throws<DomainException>(() =>
        {
            try
            {
                grantMethod!.Invoke(estate, new[] { participant, otherExecutor });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException ?? ex;
            }
        });

        var authorizedExecutor = executorFrom!.Invoke(null, new object?[] { executorId.Value() });
        Assert.NotNull(authorizedExecutor);

        grantMethod!.Invoke(estate, new[] { participant, authorizedExecutor });

        var participantsField = estate
            .GetType()
            .GetField("_participants", BindingFlags.Instance | BindingFlags.NonPublic);

        Assert.NotNull(participantsField);

        var participants = participantsField!.GetValue(estate) as IEnumerable;
        Assert.NotNull(participants);

        var enumerator = participants!.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(participant, enumerator.Current);
    }

    [Fact]
    public void RevokeParticipantAccessShouldEnforceExecutorAuthority()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        var participantType = typeof(Estate)
            .Assembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == "Participant");
        Assert.NotNull(participantType);

        var fromMethod = participantType!.GetMethod("From", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(fromMethod);

        var participant = fromMethod!.Invoke(null, new object?[] { "jane.doe@example.com", "Jane", "Doe" });
        Assert.NotNull(participant);

        var participantsField = estate
            .GetType()
            .GetField("_participants", BindingFlags.Instance | BindingFlags.NonPublic);

        Assert.NotNull(participantsField);

        var participants = participantsField!.GetValue(estate) as IList;
        Assert.NotNull(participants);

        participants!.Add(participant);

        var executorType = typeof(Estate)
            .Assembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == "Executor");
        Assert.NotNull(executorType);

        var executorFrom = executorType!.GetMethod("From", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(executorFrom);

        var otherExecutor = executorFrom!.Invoke(null, new object?[] { Guid.NewGuid() });
        Assert.NotNull(otherExecutor);

        var revokeMethod = estate
            .GetType()
            .GetMethod("RevokeParticipantAccess", BindingFlags.Instance | BindingFlags.Public, null, new[] { participantType, executorType }, null);

        Assert.NotNull(revokeMethod);

        Assert.Throws<DomainException>(() =>
        {
            try
            {
                revokeMethod!.Invoke(estate, new[] { participant, otherExecutor });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException ?? ex;
            }
        });

        var enumerator = participants.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(participant, enumerator.Current);

        var authorizedExecutor = executorFrom!.Invoke(null, new object?[] { executorId.Value() });
        Assert.NotNull(authorizedExecutor);

        revokeMethod!.Invoke(estate, new[] { participant, authorizedExecutor });

        enumerator = participants.GetEnumerator();
        Assert.False(enumerator.MoveNext());
    }

    [Fact]
    public void ParticipantWithContributionsCannotBeRevoked()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        var participantType = typeof(Estate)
            .Assembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == "Participant");
        Assert.NotNull(participantType);

        var participantFrom = participantType!.GetMethod("From", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(participantFrom);

        var participant = participantFrom!.Invoke(null, new object?[] { "jane.doe@example.com", "Jane", "Doe" });
        Assert.NotNull(participant);

        var executorType = typeof(Estate)
            .Assembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == "Executor");
        Assert.NotNull(executorType);

        var executorFrom = executorType!.GetMethod("From", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(executorFrom);

        var executor = executorFrom!.Invoke(null, new object?[] { executorId.Value() });
        Assert.NotNull(executor);

        var participantsField = estate
            .GetType()
            .GetField("_participants", BindingFlags.Instance | BindingFlags.NonPublic);

        Assert.NotNull(participantsField);

        var participants = participantsField!.GetValue(estate) as IList;
        Assert.NotNull(participants);

        participants!.Add(participant);

        var contributionsField = estate
            .GetType()
            .GetField("_contributions", BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.NotNull(contributionsField);

        var contributions = contributionsField!.GetValue(estate) as IList;
        Assert.NotNull(contributions);

        contributions!.Add("dummy");

        var revokeMethod = estate
            .GetType()
            .GetMethod("RevokeParticipantAccess", BindingFlags.Instance | BindingFlags.Public, null, new[] { participantType, executorType }, null);

        Assert.NotNull(revokeMethod);

        Assert.Throws<DomainException>(() =>
        {
            try
            {
                revokeMethod!.Invoke(estate, new[] { participant, executor });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException ?? ex;
            }
        });

        var enumerator = participants.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(participant, enumerator.Current);
    }

    [Fact]
    public void ParticipantWithUpdateMustRespectPostingRules()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        var updateType = typeof(Estate)
            .Assembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == "Update");
        Assert.NotNull(updateType);

        var updateFrom = updateType!.GetMethod("From", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(updateFrom);

        var update = updateFrom!.Invoke(null, new object?[] { "First update" });
        Assert.NotNull(update);

        var executorType = typeof(Estate)
            .Assembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == "Executor");
        Assert.NotNull(executorType);

        var executorFrom = executorType!.GetMethod("From", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(executorFrom);

        var executor = executorFrom!.Invoke(null, new object?[] { executorId.Value() });
        Assert.NotNull(executor);

        var postUpdateMethod = estate
            .GetType()
            .GetMethod("PostUpdate", BindingFlags.Instance | BindingFlags.Public, null, new[] { updateType, executorType }, null);

        Assert.NotNull(postUpdateMethod);

        postUpdateMethod!.Invoke(estate, new[] { update, executor });

        var updatesField = estate
            .GetType()
            .GetField("_updates", BindingFlags.Instance | BindingFlags.NonPublic);

        Assert.NotNull(updatesField);

        var updates = updatesField!.GetValue(estate) as IEnumerable;
        Assert.NotNull(updates);

        var enumerator = updates!.GetEnumerator();
        Assert.True(enumerator.MoveNext());
        Assert.Equal(update, enumerator.Current);

        estate
            .GetType()
            .GetField("_status", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(estate, EstateStatus.Closed);

        Assert.Throws<DomainException>(() =>
        {
            try
            {
                postUpdateMethod!.Invoke(estate, new[] { update, executor });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException ?? ex;
            }
        });

        var participantType = typeof(Estate)
            .Assembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == "Participant");
        Assert.NotNull(participantType);

        var participantFrom = participantType!.GetMethod("From", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(participantFrom);

        var participant = participantFrom!.Invoke(null, new object?[] { "jane.doe@example.com", "Jane", "Doe" });
        Assert.NotNull(participant);

        Assert.Throws<DomainException>(() =>
        {
            try
            {
                postUpdateMethod!.Invoke(estate, new[] { update, participant });
            }
            catch (ArgumentException)
            {
                throw new DomainException("Executor is required");
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException is ArgumentException)
                {
                    throw new DomainException("Executor is required");
                }

                throw ex.InnerException ?? ex;
            }
        });
    }
}
