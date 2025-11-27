using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application;

public sealed class RenameEstateFlow(IEstates estates)
{
    public async Task<EstateRenamed> Execute(RenameEstate input)
    {
        var newName = EstateName.From(input.NewName);

        await estates.Rename(input.EstateId, newName);

        return new EstateRenamed(input.EstateId);
    }
}
