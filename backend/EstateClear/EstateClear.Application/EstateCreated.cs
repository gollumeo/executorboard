using EstateClear.Domain.Estates;

namespace EstateClear.Application;

public sealed class EstateCreated(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
