using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application.Commands;

public sealed class EstateRenamed(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
