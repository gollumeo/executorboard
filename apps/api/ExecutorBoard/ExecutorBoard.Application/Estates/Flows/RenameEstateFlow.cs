using ExecutorBoard.Application.Estates.Commands;
using ExecutorBoard.Application.Estates.Errors;
using ExecutorBoard.Application.Estates.Ports;
using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Flows;

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
            throw new Exception(EstateErrors.NameAlreadyExistsForExecutor);
        }

        estate.RenameTo(newName);

        await estates.Save(estate);

        return new EstateRenamed(input.EstateId);
    }
}
