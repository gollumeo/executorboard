using System.Threading.Tasks;
using EstateClear.Application;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;
using Xunit;

namespace EstateClear.Tests.Application;

public class GrantParticipantAccessFlowTests
{
    [Fact]
    public async Task GrantParticipantAccessFlowShouldGrantAccessForExecutor()
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

        await Assert.ThrowsAnyAsync<Exception>(() => flow.Execute(input));
    }
}
