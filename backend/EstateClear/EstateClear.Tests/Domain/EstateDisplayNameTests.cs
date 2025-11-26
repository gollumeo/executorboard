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

        var action = () => Estate.Create(estateId, executorId, EstateName.From(displayName));

        Assert.ThrowsAny<Exception>(action);
    }

    [Fact]
    public void AnEstateDisplayNameShouldBeTrimmed()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = "  Estate Alpha  ";

        var estate = Estate.Create(estateId, executorId, EstateName.From(displayName));

        Assert.Equal("Estate Alpha", estate.DisplayName.Value());
    }

    [Fact]
    public void AnEstateDisplayNameCannotBeTooShort()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = "A";

        var action = () => Estate.Create(estateId, executorId, EstateName.From(displayName));

        Assert.ThrowsAny<Exception>(action);
    }

    [Fact]
    public void AnEstateNameShouldNormalizeItself()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = "  eSTaTe   ALpha  ";

        var estate = Estate.Create(estateId, executorId, EstateName.From(displayName));

        Assert.Equal("Estate Alpha", estate.DisplayName.Value());
    }

    [Fact]
    public void NormalizingTheSameEstateNameTwiceShouldProduceIdenticalResults()
    {
        var estateId1 = EstateId.From(Guid.NewGuid());
        var estateId2 = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var displayName = "  eSTaTe   ALpha  ";

        var estate1 = Estate.Create(estateId1, executorId, EstateName.From(displayName));
        var estate2 = Estate.Create(estateId2, executorId, EstateName.From(displayName));

        Assert.Equal(estate1.DisplayName.Value(), estate2.DisplayName.Value());
    }

    [Fact]
    public void AnEstateNameShouldBehaveAsAValue()
    {
        var estateId1 = EstateId.From(Guid.NewGuid());
        var estateId2 = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var rawName1 = "estate alpha";
        var rawName2 = "  Estate   Alpha  ";

        var estate1 = Estate.Create(estateId1, executorId, EstateName.From(rawName1));
        var estate2 = Estate.Create(estateId2, executorId, EstateName.From(rawName2));

        Assert.Equal(estate1.DisplayName.Value(), estate2.DisplayName.Value());
    }

    [Fact]
    public void AnEstateNameShouldBeComparableIndependently()
    {
        var rawName1 = "estate alpha";
        var rawName2 = "  Estate   Alpha  ";

        var normalized1 = EstateName.From(rawName1);
        var normalized2 = EstateName.From(rawName2);

        Assert.Equal(normalized1.Value(), normalized2.Value());
    }

    [Fact]
    public void AnEstateNameShouldBeAFirstClassDomainValue()
    {
        var raw1 = "  eSTaTe   alpha ";
        var raw2 = "ESTATE ALPHA";

        var normalized1 = EstateName.From(raw1);
        var normalized2 = EstateName.From(raw2);

        Assert.Equal(normalized1.Value(), normalized2.Value());
        Assert.Equal(normalized1.Value(), normalized2.Value());
    }
}
