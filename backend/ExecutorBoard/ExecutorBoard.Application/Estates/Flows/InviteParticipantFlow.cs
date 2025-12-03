using ExecutorBoard.Application.Estates.Commands;
using ExecutorBoard.Application.Estates.Ports;
using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Flows;

public sealed class InviteParticipantFlow(IEstates estates, IParticipantInvitations invitations)
{
    public async Task<InviteParticipantResult> Execute(InviteParticipant input)
    {
        var estateId = EstateId.From(input.EstateId);
        var estate = await estates.Load(estateId);
        if (estate is null)
        {
            throw new Exception("Estate not found");
        }

        var participant = estate.AddPendingParticipant(input.Email);
        var token = InvitationToken.From(Guid.NewGuid().ToString());

        await invitations.Store(token, participant.Id, estate.Id);
        await estates.Save(estate);

        return new InviteParticipantResult(participant.Id, token);
    }
}

public sealed class InviteParticipantResult(ParticipantId participantId, InvitationToken invitationToken)
{
    public ParticipantId ParticipantId { get; } = participantId;

    public InvitationToken InvitationToken { get; } = invitationToken;
}
