using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Queries;

public sealed class ProjectSingleEstate(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
