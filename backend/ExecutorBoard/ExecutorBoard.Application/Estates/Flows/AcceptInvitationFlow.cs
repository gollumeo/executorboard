using ExecutorBoard.Application.Estates.Commands;
using ExecutorBoard.Application.Estates.Ports;
using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Flows;

public sealed class AcceptInvitationFlow(IEstates estates, IParticipantInvitations invitations)
{
    public async Task<AcceptInvitationResult> Execute(AcceptInvitation input)
    {
        var token = InvitationToken.From(input.Token);
        var lookup = await invitations.FindByToken(token);
        if (lookup is null)
        {
            throw new Exception("Invalid token");
        }

        var estate = await estates.Load(lookup.EstateId);
        if (estate is null)
        {
            throw new Exception("Estate not found");
        }

        if (!estate.Participants().Any(p => p.Id.Equals(lookup.ParticipantId)))
        {
            throw new Exception("Participant not in estate");
        }

        var participant = estate.Participants().First(p => p.Id.Equals(lookup.ParticipantId));
        if (participant.Status != ParticipantStatus.Pending)
        {
            throw new Exception("Invalid participant status");
        }
        if (participant.Status == ParticipantStatus.Active)
        {
            throw new Exception("Participant already active");
        }

        participant = estate.ActivateParticipant(lookup.ParticipantId);

        await invitations.Remove(token);
        await estates.Save(estate);

        return new AcceptInvitationResult(participant.Id);
    }
}

public sealed class AcceptInvitationResult(ParticipantId participantId)
{
    public ParticipantId ParticipantId { get; } = participantId;
}
