namespace EstateClear.Domain.Estates;

public class Estate
{
    private Estate(EstateId id, ExecutorId executorId, string displayName)
    {
        Id = id;
        ExecutorId = executorId;
        DisplayName = displayName;
        Status = EstateStatus.Active;
    }

    public EstateId Id { get; }

    public ExecutorId ExecutorId { get; }

    public string DisplayName { get; }

    public EstateStatus Status { get; }

    public static Estate Create(EstateId id, ExecutorId executorId, string displayName)
    {
        if (executorId is null)
        {
            throw new DomainException("Executor is required");
        }

        if (executorId.Value == Guid.Empty)
        {
            throw new DomainException("Executor is required");
        }

        if (string.IsNullOrWhiteSpace(displayName))
        {
            throw new DomainException("Display name is required");
        }

        var trimmedDisplayName = displayName.Trim();

        if (trimmedDisplayName.Length < 2)
        {
            throw new DomainException("Display name is too short");
        }

        return new Estate(id, executorId, trimmedDisplayName);
    }
}
