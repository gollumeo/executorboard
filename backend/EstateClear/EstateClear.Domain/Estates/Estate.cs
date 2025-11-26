namespace EstateClear.Domain.Estates;

using System.Linq;

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

        var normalizedDisplayName = string.Join(
            " ",
            trimmedDisplayName
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(word =>
                {
                    var lower = word.ToLowerInvariant();
                    return char.ToUpperInvariant(lower[0]) + lower[1..];
                }));

        return new Estate(id, executorId, normalizedDisplayName);
    }

    public static string NormalizeName(string displayName)
    {
        if (string.IsNullOrWhiteSpace(displayName))
        {
            throw new DomainException("Display name is required");
        }

        var trimmedDisplayName = displayName.Trim();

        if (trimmedDisplayName.Length < 2)
        {
            throw new DomainException("Display name is too short");
        }

        return string.Join(
            " ",
            trimmedDisplayName
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(word =>
                {
                    var lower = word.ToLowerInvariant();
                    return char.ToUpperInvariant(lower[0]) + lower[1..];
                }));
    }
}
