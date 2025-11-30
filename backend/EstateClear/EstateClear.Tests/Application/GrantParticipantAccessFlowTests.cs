using System.Threading.Tasks;
using EstateClear.Application;
using EstateClear.Domain;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;
using Xunit;

namespace EstateClear.Tests.Application;

public class GrantParticipantAccessFlowTests
{
    [Fact]
    public async Task GrantAccessShouldAddParticipantAndSaveEstate()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var participant = Participant.From("john.doe@example.com", "John", "Doe");
        var executor = Executor.From(executorId.Value());
        var input = new GrantParticipantAccess(estateId, participant, executor);
        var flow = new GrantParticipantAccessFlow(estates);

        await flow.Execute(input);

        Assert.Single(estates.SavedEstates);
    }

    [Fact]
    public async Task GrantAccessShouldFailWhenEstateNotFound()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var participant = Participant.From("jane.doe@example.com", "Jane", "Doe");
        var executor = Executor.From(Guid.NewGuid());
        var estates = new EstatesFake();
        var input = new GrantParticipantAccess(estateId, participant, executor);
        var flow = new GrantParticipantAccessFlow(estates);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAnyAsync<Exception>(action);
        Assert.Empty(estates.SavedEstates);
    }

    [Fact]
    public async Task GrantAccessShouldFailWhenExecutorIsNotAuthorized()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var participant = Participant.From("johnny.doe@example.com", "Johnny", "Doe");
        var unauthorizedExecutor = Executor.From(Guid.NewGuid());
        var input = new GrantParticipantAccess(estateId, participant, unauthorizedExecutor);
        var flow = new GrantParticipantAccessFlow(estates);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAsync<DomainException>(action);
        Assert.Empty(estates.SavedEstates);
    }
}
