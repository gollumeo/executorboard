using EstateClear.Domain.Estates;

namespace EstateClear.Application;

public interface IEstates
{
    Task Add(EstateId estateId, ExecutorId executorId, string displayName);

    Task<bool> ExistsWithName(ExecutorId executorId, string displayName);
}
