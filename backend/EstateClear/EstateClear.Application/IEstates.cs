using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application;

public interface IEstates
{
    Task Add(EstateId estateId, ExecutorId executorId, string displayName);

    Task<bool> ExistsWithName(ExecutorId executorId, EstateName estateName);

    Task Rename(EstateId estateId, EstateName newName);

    Task<ExecutorId> Executor(EstateId estateId);
}
