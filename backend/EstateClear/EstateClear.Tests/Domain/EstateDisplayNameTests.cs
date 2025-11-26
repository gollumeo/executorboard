using EstateClear.Domain.Estates;
using Xunit;

namespace EstateClear.Tests.Domain;

public class EstateDisplayNameTests
{
    [Fact]
    public void AnEstateDisplayNameCannotBeEmpty()
    {
        var displayName = "   ";
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());

        var action = () => Estate.Create(estateId, executorId, displayName);

        Assert.ThrowsAny<Exception>(action);
    }

    [Fact]
    public void AnEstateDisplayNameShouldBeTrimmed()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = "  Estate Alpha  ";

        var estate = Estate.Create(estateId, executorId, displayName);

        Assert.Equal("Estate Alpha", estate.DisplayName);
    }

    [Fact]
    public void AnEstateDisplayNameCannotBeTooShort()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = "A";

        var action = () => Estate.Create(estateId, executorId, displayName);

        Assert.ThrowsAny<Exception>(action);
    }
}
