using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application;

public sealed class AddParticipantToEstateFlow(IEstates estates)
{
    public async Task<EstateParticipantAdded> Execute(AddParticipantToEstate input)
    {
        var estate = await estates.Load(input.EstateId);
        if (estate is null)
        {
            throw new Exception("Estate not found");
        }

        estate.AddParticipant();

        await estates.Save(estate);

        return new EstateParticipantAdded(estate.Id);
    }
}
