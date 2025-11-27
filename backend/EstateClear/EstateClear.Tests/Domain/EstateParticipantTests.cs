using System.Reflection;
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
}
