using System.Reflection;
using EstateClear.Domain;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Tests.Domain;

public class EstateRenameRulesTests
{
    [Fact]
    public void RenamingAClosedEstateShouldBeForbidden()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        typeof(Estate)
            .GetField("_status", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(estate, EstateStatus.Closed);

        var action = () => estate.RenameTo(EstateName.From("Estate Beta"));

        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void RenamingAnActiveEstateShouldUpdateItsDisplayName()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        estate.RenameTo(EstateName.From("Estate Beta"));

        Assert.Equal("Estate Beta", estate.DisplayName().Value());
    }

    [Fact]
    public void RenamingEstateShouldBeForbiddenWhenParticipantsExist()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));

        var participant = Participant.From("john.doe@example.com", "John", "Doe");
        var executor = Executor.From(executorId.Value());
        estate.GrantParticipantAccess(participant, executor);

        var action = () => estate.RenameTo(EstateName.From("Estate Beta"));

        Assert.Throws<DomainException>(action);
        Assert.Equal("Estate Alpha", estate.DisplayName().Value());
    }
}
