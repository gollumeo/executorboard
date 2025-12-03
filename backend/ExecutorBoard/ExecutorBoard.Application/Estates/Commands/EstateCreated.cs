using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Commands;

public sealed class EstateCreated(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
