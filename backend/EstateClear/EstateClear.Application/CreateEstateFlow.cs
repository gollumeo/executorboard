using EstateClear.Domain.Estates;

namespace EstateClear.Application;

public sealed class CreateEstateFlow(IEstates estates)
{
    public async Task<EstateCreated> Execute(CreateEstate input)
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(input.ExecutorId);

        await estates.Add(estateId, executorId, input.DisplayName);

        return new EstateCreated(estateId);
    }
}
