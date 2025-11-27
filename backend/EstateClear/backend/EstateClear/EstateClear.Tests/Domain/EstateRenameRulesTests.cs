using System.Reflection;
using EstateClear.Domain;
using EstateClear.Domain.Estates;
using Xunit;

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
            .GetField("<Status>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)
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

        Assert.Equal("Estate Beta", estate.DisplayName.Value());
    }
}
