using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application;

public sealed class RenameEstate(EstateId estateId, string newName)
{
    public EstateId EstateId { get; } = estateId;

    public string NewName { get; } = newName;
}
