using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application.Commands;

public sealed class CloseEstate(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
