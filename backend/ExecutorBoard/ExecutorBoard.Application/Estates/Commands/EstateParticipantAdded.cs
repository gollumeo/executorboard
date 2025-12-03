using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Commands;

public sealed class EstateParticipantAdded(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
