using ExecutorBoard.Application.Estates.Ports;
using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Tests.Application;

public class ParticipantInvitationsFake : IParticipantInvitations
{
    public List<(InvitationToken Token, ParticipantId ParticipantId, EstateId EstateId)> StoredInvitations { get; } = new();

    public Task Store(InvitationToken token, ParticipantId participantId, EstateId estateId)
    {
        StoredInvitations.Add((token, participantId, estateId));
        return Task.CompletedTask;
    }

    public Task<InvitationLookup?> FindByToken(InvitationToken token)
    {
        var match = StoredInvitations.FirstOrDefault(item => item.Token.Equals(token));
        if (match.Token is null)
        {
            return Task.FromResult<InvitationLookup?>(null);
        }

        return Task.FromResult<InvitationLookup?>(new InvitationLookup(match.ParticipantId, match.EstateId));
    }

    public Task Remove(InvitationToken token)
    {
        StoredInvitations.RemoveAll(item => item.Token.Equals(token));
        return Task.CompletedTask;
    }

    public bool Contains(InvitationToken token)
    {
        return StoredInvitations.Any(item => item.Token.Equals(token));
    }
}
