using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Commands;

public sealed class GrantParticipantAccess(EstateId estateId, Participant participant, Executor executor)
{
    public EstateId EstateId { get; } = estateId;
    public Participant Participant { get; } = participant;
    public Executor Executor { get; } = executor;
}
