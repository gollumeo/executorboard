using System.Collections;
using System.Reflection;
using System.Linq;
using EstateClear.Domain;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;
using Xunit;

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

        var duplicate = fromMethod!.Invoke(null, new object?[] { "john.doe@example.com", "Johnny", "Doe" });
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
}
