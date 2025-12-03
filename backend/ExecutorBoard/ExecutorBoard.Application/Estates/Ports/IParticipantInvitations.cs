using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Ports;

public interface IParticipantInvitations
{
    Task Store(InvitationToken token, ParticipantId participantId, EstateId estateId);

    Task<InvitationLookup?> FindByToken(InvitationToken token);

    Task Remove(InvitationToken token);
}

public sealed record InvitationLookup(ParticipantId ParticipantId, EstateId EstateId);
