using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Commands;

public sealed class RenameEstate(EstateId estateId, string newName)
{
    public EstateId EstateId { get; } = estateId;

    public string NewName { get; } = newName;
}
