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

    [Fact]
    public void AnEstateNameShouldNormalizeItself()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = "  eSTaTe   ALpha  ";

        var estate = Estate.Create(estateId, executorId, displayName);

        Assert.Equal("Estate Alpha", estate.DisplayName);
    }

    [Fact]
    public void NormalizingTheSameEstateNameTwiceShouldProduceIdenticalResults()
    {
        var estateId1 = EstateId.From(Guid.NewGuid());
        var estateId2 = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = "  eSTaTe   ALpha  ";

        var estate1 = Estate.Create(estateId1, executorId, displayName);
        var estate2 = Estate.Create(estateId2, executorId, displayName);

        Assert.Equal(estate1.DisplayName, estate2.DisplayName);
    }

    [Fact]
    public void AnEstateNameShouldBehaveAsAValue()
    {
        var estateId1 = EstateId.From(Guid.NewGuid());
        var estateId2 = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var rawName1 = "estate alpha";
        var rawName2 = "  Estate   Alpha  ";

        var estate1 = Estate.Create(estateId1, executorId, rawName1);
        var estate2 = Estate.Create(estateId2, executorId, rawName2);

        Assert.Equal(estate1.DisplayName, estate2.DisplayName);
    }

    [Fact]
    public void AnEstateNameShouldBeComparableIndependently()
    {
        var rawName1 = "estate alpha";
        var rawName2 = "  Estate   Alpha  ";

        var normalized1 = Estate.NormalizeName(rawName1);
        var normalized2 = Estate.NormalizeName(rawName2);

        Assert.Equal(normalized1, normalized2);
    }
}
