using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Commands;

public sealed class CloseEstate(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
