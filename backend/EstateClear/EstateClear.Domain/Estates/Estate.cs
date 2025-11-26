namespace EstateClear.Domain.Estates;

using System.Linq;

public class Estate
{
    private Estate(EstateId id, ExecutorId executorId, EstateName displayName)
    {
        Id = id;
        ExecutorId = executorId;
        DisplayName = displayName;
        Status = EstateStatus.Active;
    }

    public EstateId Id { get; }

    public ExecutorId ExecutorId { get; }

    public EstateName DisplayName { get; }

    public EstateStatus Status { get; }

    public static Estate Create(EstateId id, ExecutorId executorId, string displayName)
    {
        if (executorId is null)
        {
            throw new DomainException("Executor is required");
        }

        if (executorId.Value() == Guid.Empty)
        {
            throw new DomainException("Executor is required");
        }

        var estateName = EstateName.From(displayName);

        return new Estate(id, executorId, estateName);
    }
}
