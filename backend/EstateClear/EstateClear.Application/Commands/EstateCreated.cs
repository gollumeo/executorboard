using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application.Commands;

public sealed class EstateCreated(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
