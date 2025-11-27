using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application;

public sealed class EstateRenamed(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
