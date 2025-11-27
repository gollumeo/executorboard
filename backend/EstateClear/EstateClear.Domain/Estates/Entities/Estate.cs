using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Domain.Estates.Entities;

public class Estate
{
    private Estate(EstateId id, ExecutorId executorId, EstateName displayName)
    {
        Id = id;
        ExecutorId = executorId;
        _displayName = displayName;
        Status = EstateStatus.Active;
    }

    private EstateName _displayName;

    public EstateId Id { get; }

    public ExecutorId ExecutorId { get; }

    public EstateName DisplayName() => _displayName;

    public EstateStatus Status { get; }

    public void RenameTo(EstateName newName)
    {
        if (Status == EstateStatus.Closed)
        {
            throw new DomainException("Estate is closed");
        }

        _displayName = newName;
    }

    public static Estate Create(EstateId id, ExecutorId executorId, EstateName estateName)
    {
        if (executorId is null)
        {
            throw new DomainException("Executor is required");
        }

        if (executorId.Value() == Guid.Empty)
        {
            throw new DomainException("Executor is required");
        }

        return new Estate(id, executorId, estateName);
    }
}
