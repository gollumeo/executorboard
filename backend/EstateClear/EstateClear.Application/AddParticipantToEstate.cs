using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application;

public sealed class AddParticipantToEstate(EstateId estateId)
{
    public EstateId EstateId { get; } = estateId;
}
