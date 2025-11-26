using EstateClear.Domain.Estates;

namespace EstateClear.Application;

public sealed class CreateEstateFlow(IEstates estates)
{
    public async Task<EstateCreated> Execute(CreateEstate input)
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(input.ExecutorId);
        var estateName = EstateName.From(input.DisplayName);
        var estate = Estate.Create(estateId, executorId, estateName);

        await estates.Add(estate.Id, estate.ExecutorId, estate.DisplayName.Value());

        return new EstateCreated(estate.Id);
    }
}
