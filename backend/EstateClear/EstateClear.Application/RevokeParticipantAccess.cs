using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application;

public sealed class RevokeParticipantAccess(EstateId estateId, Participant participant, Executor executor)
{
    public EstateId EstateId { get; } = estateId;
    public Participant Participant { get; } = participant;
    public Executor Executor { get; } = executor;
}
