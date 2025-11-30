using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application.Queries;

public sealed class ProjectParticipantsOfEstate(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
