using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EstateClear.Application;
using EstateClear.Domain.Estates;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Tests.Application;

public class EstatesFake : IEstates
{
    public List<(EstateId EstateId, ExecutorId ExecutorId, string DisplayName)> AddedEstates { get; } = new();

    public Task Add(EstateId estateId, ExecutorId executorId, string displayName)
    {
        AddedEstates.Add((estateId, executorId, displayName));
        return Task.CompletedTask;
    }

    public Task<bool> ExistsWithName(ExecutorId executorId, EstateName estateName)
    {
        var exists = AddedEstates.Any(e =>
            e.ExecutorId.Value() == executorId.Value() &&
            EstateName.From(e.DisplayName).Value() == estateName.Value());

        return Task.FromResult(exists);
    }
}
