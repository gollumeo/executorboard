using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EstateClear.Application;
using EstateClear.Domain.Estates;

namespace EstateClear.Tests.Application;

public class EstatesFake : IEstates
{
    public List<(EstateId EstateId, ExecutorId ExecutorId, string DisplayName)> AddedEstates { get; } = new();

    public Task Add(EstateId estateId, ExecutorId executorId, string displayName)
    {
        AddedEstates.Add((estateId, executorId, displayName));
        return Task.CompletedTask;
    }

    public Task<bool> ExistsWithName(ExecutorId executorId, string displayName)
    {
        var exists = AddedEstates.Any(e =>
            e.ExecutorId.Value() == executorId.Value() &&
            e.DisplayName == displayName);

        return Task.FromResult(exists);
    }
}
