using EstateClear.Application.DTOs;
using EstateClear.Application.Queries;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Tests.Application;

public class ProjectParticipantsOfEstateFlowTests
{
    [Fact]
    public async Task ProjectingParticipantsOfExistingEstateShouldReturnProjections()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var executor = Executor.From(executorId.Value());
        var participantA = Participant.From("john.doe@example.com", "John", "Doe");
        var participantB = Participant.From("jane.doe@example.com", "Jane", "Doe");
        estate.GrantParticipantAccess(participantA, executor);
        estate.GrantParticipantAccess(participantB, executor);
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var input = new ProjectParticipantsOfEstate(estateId);
        var flow = new ProjectParticipantsOfEstateFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.All(result, item => Assert.IsType<EstateParticipantProjection>(item));
        Assert.Collection(result,
            item =>
            {
                Assert.Equal("john.doe@example.com", item.Email);
                Assert.Equal("John", item.FirstName);
                Assert.Equal("Doe", item.LastName);
            },
            item =>
            {
                Assert.Equal("jane.doe@example.com", item.Email);
                Assert.Equal("Jane", item.FirstName);
                Assert.Equal("Doe", item.LastName);
            });
    }

    [Fact]
    public async Task ProjectingParticipantsWhenEstateNotFoundShouldReturnEmptyList()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var estates = new EstatesFake
        {
            LoadedEstate = null
        };
        var input = new ProjectParticipantsOfEstate(estateId);
        var flow = new ProjectParticipantsOfEstateFlow(estates);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
