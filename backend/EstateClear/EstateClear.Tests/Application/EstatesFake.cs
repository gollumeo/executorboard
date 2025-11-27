using EstateClear.Application;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Tests.Application;

public class EstatesFake : IEstates
{
    public List<(EstateId EstateId, ExecutorId ExecutorId, string DisplayName)> AddedEstates { get; } = new();
    public List<(EstateId EstateId, EstateName NewName)> RenamedEstates { get; } = new();

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

    public Task Rename(EstateId estateId, EstateName newName)
    {
        RenamedEstates.Add((estateId, newName));
        return Task.CompletedTask;
    }

    public Task<ExecutorId> Executor(EstateId estateId)
    {
        var match = AddedEstates.First(e => e.EstateId == estateId);
        return Task.FromResult(match.ExecutorId);
    }
}
