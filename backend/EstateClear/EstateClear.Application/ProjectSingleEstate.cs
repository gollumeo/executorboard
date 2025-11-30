using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application;

public sealed class ProjectSingleEstate(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
