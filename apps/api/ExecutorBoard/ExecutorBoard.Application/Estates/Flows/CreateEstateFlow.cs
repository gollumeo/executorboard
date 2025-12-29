using ExecutorBoard.Application.Estates.Commands;
using ExecutorBoard.Application.Estates.Errors;
using ExecutorBoard.Application.Estates.Ports;
using ExecutorBoard.Domain.Estates.Entities;
using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Flows;

public sealed class CreateEstateFlow(IEstates estates)
{
    public async Task<EstateCreated> Execute(CreateEstate input)
    {
        var estateId = EstateId.From(Guid.NewGuid());
        var executorId = ExecutorId.From(input.ExecutorId);
        var estateName = EstateName.From(input.DisplayName);
        var exists = await estates.ExistsWithName(executorId, estateName);

        if (exists)
        {
            throw new Exception(EstateErrors.NameAlreadyExistsForExecutor);
        }

        var estate = Estate.Create(estateId, executorId, estateName);

        await estates.Add(estate.Id, estate.ExecutorId, estate.DisplayName().Value());

        return new EstateCreated(estate.Id);
    }
}
