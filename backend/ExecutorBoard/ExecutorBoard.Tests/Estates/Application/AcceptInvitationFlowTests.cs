using ExecutorBoard.Application.Estates.Commands;
using ExecutorBoard.Application.Estates.Flows;
using ExecutorBoard.Domain.Estates.ValueObjects;
using ExecutorBoard.Tests.Application;

namespace ExecutorBoard.Tests.Estates.Application;

public class AcceptInvitationFlowTests
{
    [Fact]
    public async Task AcceptInvitationShouldActivateParticipantAndInvalidateToken()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = ExecutorBoard.Domain.Estates.Entities.Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var participant = estate.AddPendingParticipant("user@example.com");
        var token = InvitationToken.From(Guid.NewGuid().ToString());
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var invitations = new ParticipantInvitationsFake();
        await invitations.Store(token, participant.Id, estateId);
        var input = new AcceptInvitation(token.Value());
        var flow = new AcceptInvitationFlow(estates, invitations);

        var result = await flow.Execute(input);

        Assert.Equal(ParticipantStatus.Active, estate.Participants().Single().Status);
        Assert.False(invitations.Contains(token));
        Assert.NotNull(result);
        Assert.Equal(participant.Id.Value(), result.ParticipantId.Value());
    }

    [Fact]
    public async Task AcceptInvitationWithInvalidTokenShouldThrow()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = ExecutorBoard.Domain.Estates.Entities.Estate.Create(estateId, executorId, EstateName.From("Estate Beta"));
        estate.AddPendingParticipant("user@example.com");
        var token = InvitationToken.From(Guid.NewGuid().ToString());
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var invitations = new ParticipantInvitationsFake();
        var input = new AcceptInvitation(token.Value());
        var flow = new AcceptInvitationFlow(estates, invitations);

        await Assert.ThrowsAnyAsync<Exception>(() => flow.Execute(input));
    }

    [Fact]
    public async Task AcceptInvitationWhenParticipantAlreadyActiveShouldThrow()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = ExecutorBoard.Domain.Estates.Entities.Estate.Create(estateId, executorId, EstateName.From("Estate Gamma"));
        var participant = estate.AddPendingParticipant("user@example.com");
        estate.ActivateParticipant(participant.Id);
        var token = InvitationToken.From(Guid.NewGuid().ToString());
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var invitations = new ParticipantInvitationsFake();
        await invitations.Store(token, participant.Id, estateId);
        var input = new AcceptInvitation(token.Value());
        var flow = new AcceptInvitationFlow(estates, invitations);

        await Assert.ThrowsAnyAsync<Exception>(() => flow.Execute(input));
    }

    [Fact]
    public async Task AcceptInvitationWhenParticipantDoesNotBelongToEstateShouldThrow()
    {
        var estateAId = EstateId.From(Guid.NewGuid());
        var estateBId = EstateId.From(Guid.NewGuid());
        var executorAId = ExecutorId.From(Guid.NewGuid());
        var executorBId = ExecutorId.From(Guid.NewGuid());
        var estateA = ExecutorBoard.Domain.Estates.Entities.Estate.Create(estateAId, executorAId, EstateName.From("Estate A"));
        var estateB = ExecutorBoard.Domain.Estates.Entities.Estate.Create(estateBId, executorBId, EstateName.From("Estate B"));
        var participant = estateB.AddPendingParticipant("user@example.com");
        var token = InvitationToken.From(Guid.NewGuid().ToString());
        var estates = new EstatesFake();
        estates.EstatesById[estateAId] = estateA;
        estates.EstatesById[estateBId] = estateB;
        var invitations = new ParticipantInvitationsFake();
        await invitations.Store(token, participant.Id, estateAId);
        var input = new AcceptInvitation(token.Value());
        var flow = new AcceptInvitationFlow(estates, invitations);

        await Assert.ThrowsAnyAsync<Exception>(() => flow.Execute(input));
    }
}
