using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application.Commands;

public sealed class RenameEstateFlow(IEstates estates)
{
    public async Task<EstateRenamed> Execute(RenameEstate input)
    {
        var estate = await estates.Load(input.EstateId);
        if (estate is null)
        {
            throw new Exception("Estate not found");
        }

        var newName = EstateName.From(input.NewName);

        if (estate.DisplayName().Value() == newName.Value())
        {
            return new EstateRenamed(input.EstateId);
        }

        var exists = await estates.ExistsWithName(estate.ExecutorId, newName);

        if (exists && estate.DisplayName().Value() != newName.Value())
        {
            throw new Exception("Estate name already exists for executor");
        }

        estate.RenameTo(newName);

        await estates.Save(estate);

        return new EstateRenamed(input.EstateId);
    }
}
