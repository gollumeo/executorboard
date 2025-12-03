using ExecutorBoard.Application.Estates.Commands;
using ExecutorBoard.Application.Estates.Flows;
using ExecutorBoard.Domain.Estates.Entities;
using ExecutorBoard.Domain.Estates.ValueObjects;
using ExecutorBoard.Tests.Application;

namespace ExecutorBoard.Tests.Estates.Application;

public class InviteParticipantFlowTests
{
    [Fact]
    public async Task InviteParticipantShouldCreatePendingParticipantAndReturnInvitationToken()
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(Guid.NewGuid());
        var estate = Estate.Create(estateId, executorId, EstateName.From("Estate Alpha"));
        var estates = new EstatesFake();
        estates.EstatesById[estateId] = estate;
        var invitations = new ParticipantInvitationsFake();
        var input = new InviteParticipant("  NEWUSER@example.com  ", estateId.Value(), executorId.Value());
        var flow = new InviteParticipantFlow(estates, invitations);

        var result = await flow.Execute(input);

        var participant = estate.Participants().Single();
        Assert.Equal("newuser@example.com", participant.Email());
        Assert.Equal(ParticipantStatus.Pending, participant.Status);
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.ParticipantId.Value());
        Assert.False(string.IsNullOrEmpty(result.InvitationToken.Value()));
        Assert.Single(invitations.StoredInvitations);
        Assert.Equal(result.InvitationToken.Value(), invitations.StoredInvitations.Single().Token.Value());
    }
}
