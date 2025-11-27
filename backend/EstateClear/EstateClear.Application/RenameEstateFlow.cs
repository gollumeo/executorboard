using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application;

public sealed class RenameEstateFlow(IEstates estates)
{
    public async Task<EstateRenamed> Execute(RenameEstate input)
    {
        var executorId = await estates.Executor(input.EstateId);
        var newName = EstateName.From(input.NewName);

        var exists = await estates.ExistsWithName(executorId, newName);

        if (exists)
        {
            throw new Exception("Estate name already exists for executor");
        }

        await estates.Rename(input.EstateId, newName);

        return new EstateRenamed(input.EstateId);
    }
}
