using System.Collections.Generic;
using ExecutorBoard.Domain.Estates.ValueObjects;
using ExecutorBoard.Domain.Estates.Entities;

namespace ExecutorBoard.Application.Estates.Ports;

public interface IEstates
{
    Task Add(EstateId estateId, ExecutorId executorId, string displayName);

    Task<bool> ExistsWithName(ExecutorId executorId, EstateName estateName);

    Task Rename(EstateId estateId, EstateName newName);

    Task<ExecutorId> Executor(EstateId estateId);

    Task<EstateName> NameOf(EstateId estateId);

    Task<Estate?> Load(EstateId estateId);

    Task Save(Estate estate);

    Task<IReadOnlyList<Estate>> ByExecutor(ExecutorId executorId);
}
