using EstateClear.Application;
using EstateClear.Domain.Estates.Entities;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Tests.Application;

public class EstatesFake : IEstates
{
    public List<(EstateId EstateId, ExecutorId ExecutorId, string DisplayName)> AddedEstates { get; } = new();
    public List<(EstateId EstateId, EstateName NewName)> RenamedEstates { get; } = new();
    public Estate? LoadedEstate { get; set; }
    public Dictionary<EstateId, Estate> EstatesById { get; } = new();
    public List<Estate> SavedEstates { get; } = new();

    public Task Add(EstateId estateId, ExecutorId executorId, string displayName)
    {
        AddedEstates.Add((estateId, executorId, displayName));
        return Task.CompletedTask;
    }

    public Task<bool> ExistsWithName(ExecutorId executorId, EstateName estateName)
    {
        var existsInAdded = AddedEstates.Any(e =>
            e.ExecutorId.Value() == executorId.Value() &&
            EstateName.From(e.DisplayName).Value() == estateName.Value());

        var existsInLoaded = EstatesById.Values.Any(e =>
            e.ExecutorId.Value() == executorId.Value() &&
            e.DisplayName().Value() == estateName.Value());

        var exists = existsInAdded || existsInLoaded;

        return Task.FromResult(exists);
    }

    public Task Rename(EstateId estateId, EstateName newName)
    {
        RenamedEstates.Add((estateId, newName));
        return Task.CompletedTask;
    }

    public Task<ExecutorId> Executor(EstateId estateId)
    {
        if (EstatesById.TryGetValue(estateId, out var estate))
        {
            return Task.FromResult(estate.ExecutorId);
        }

        var match = AddedEstates.First(e => e.EstateId == estateId);
        return Task.FromResult(match.ExecutorId);
    }

    public Task<EstateName> NameOf(EstateId estateId)
    {
        var match = AddedEstates.First(e => e.EstateId == estateId);
        return Task.FromResult(EstateName.From(match.DisplayName));
    }

    public Task<Estate?> Load(EstateId estateId)
    {
        if (EstatesById.TryGetValue(estateId, out var estate))
        {
            return Task.FromResult<Estate?>(estate);
        }

        return Task.FromResult(LoadedEstate);
    }

    public Task Save(Estate estate)
    {
        SavedEstates.Add(estate);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Estate>> ByExecutor(ExecutorId executorId)
    {
        var matches = EstatesById
            .Values
            .Where(e => e.ExecutorId.Equals(executorId))
            .ToList()
            .AsReadOnly();

        return Task.FromResult<IReadOnlyList<Estate>>(matches);
    }
}
