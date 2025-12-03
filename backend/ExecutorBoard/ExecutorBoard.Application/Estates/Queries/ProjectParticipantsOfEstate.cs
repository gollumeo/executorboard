using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Queries;

public sealed class ProjectParticipantsOfEstate(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
